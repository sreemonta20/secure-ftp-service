using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using secure_ftp_service.Core.Models;
using secure_ftp_service.Core.Requests;
using secure_ftp_service.Core.Responses;
using secure_ftp_service.Core.Services;
using secure_ftp_service.Helpers;

namespace secure_ftp_service.Controllers
{
    [Route(ConstantSupplier.ATTRIBUTE_ROUTE)]
    [ApiController]
    [EnableCors(ConstantSupplier.CORSS_POLICY_NAME)]
    public class SftpServiceController : ControllerBase
    {
        /// <summary>
        /// Variable Declaration & Initialization
        /// </summary>
        private readonly ILogService _logService;
        private readonly IRemoteSftpService _sftpService;
        private readonly ISftpDataService _sftpDataService;
        private RemoteSftpConfigModel _serverConfigModel;
        private readonly IConfiguration _appSettings;

        /// <summary>
        /// Constructor initialization
        /// </summary>
        /// <param name="logService"></param>
        public SftpServiceController(ILogService logService, IRemoteSftpService sftpService, ISftpDataService sftpDataService, IConfiguration appSettings)
        {
            _logService = logService;
            _sftpService = sftpService;
            _sftpDataService = sftpDataService;
            _appSettings = appSettings;
            _serverConfigModel = _appSettings.GetSection(nameof(RemoteSftpConfigModel)).Get<RemoteSftpConfigModel>();
            this._sftpService.serviceInitialize(_serverConfigModel);
        }

        /// <summary>
        /// This api service (/api/SftpService/downloadsftpfiles) helps to download the server files from remote server's (rebex tiny sftp server) path location
        /// to local path location. Also after finishing the download, It stores the downloaded files path into postgresql. Next time when user wants to download
        /// the files from the server to local path, It checks if any new files exist in the server by comparing stored files (DB saved destination path and source 
        /// file path) and file creation time (DB and Server's file creation time)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(ConstantSupplier.DOWNLOAD_FILES_ROUTE_NAME)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SftpFileDetailsRes>> downloadsftpfiles()
        {
            AssistantHelper helper = new();
            string globalFullServerPath = string.Empty;
            List<string> serverFilesPathList = new();
            List<string> dbFilesPathList =  new();
            List<string> newFilesPathList = new();
            
            List<ServerFileInfo> creationTimeWiseServerFileList = new();
            List<ServerFileInfo> creationTimeWiseDbFileList = new();
            List<ServerFileInfo> newCreationTimeWiseFileInfoList = new();
            try
            {
                if (!_sftpService.IsConnected())
                {
                    _sftpService.Connect();
                }

                var localPath = @"'"+ _serverConfigModel.LocalPathDirectory + "'";
                var serverPath = @""+_serverConfigModel.ServerPathDirectory;

                _sftpService.CreateDirectoryIfNotExists(serverPath);
                if (_sftpService.DirectoryExists(serverPath))
                {
                    globalFullServerPath = Path.GetFullPath(Path.Join(@"" + _serverConfigModel.ServerPathBaseDirectory, serverPath));
                    serverFilesPathList = helper.GetFiles(globalFullServerPath);
                }

                if (helper.IsNotNull(serverFilesPathList) && helper.MoreThanZero(serverFilesPathList.Count))
                {
                    var isDownload = await _sftpService.DownloadFilesAsync(localPath, serverPath);
                    if (!isDownload)
                    {
                        return BadRequest(new SftpFileDetailsRes(false, StatusCodes.Status400BadRequest, ConstantSupplier.DOWNLOAD_FAILED_MSG));
                    }
                    creationTimeWiseServerFileList = await _sftpService.getFileInfo(globalFullServerPath);
                }
                else
                {
                    return NotFound(new SftpFileDetailsRes(false, StatusCodes.Status404NotFound, ConstantSupplier.NO_FILES_FOUND_SERVER_PATH));
                }

                var downloadedFilesPathList = helper.GetFiles(localPath.Trim('\''));
                if (helper.IsNull(downloadedFilesPathList) && helper.IsZero(downloadedFilesPathList.Count))
                {
                    return NotFound(new SftpFileDetailsRes(false, StatusCodes.Status404NotFound, ConstantSupplier.DOWNLOAD_FILES_NOT_FOUND_MSG));
                }

                var allDbStoredFiles = await _sftpDataService.GetAllFilesAsync();
                if(helper.IsNotNull(allDbStoredFiles.ListData) && helper.MoreThanZero(allDbStoredFiles.ListData.Count))
                {
                    foreach (var item in allDbStoredFiles.ListData)
                    {
                        dbFilesPathList.Add(item.DestinationFilePath);
                        creationTimeWiseDbFileList.Add(new ServerFileInfo { FileName=item.FileName, FileCreationTime=item.FileCreationTime});
                    }

                    if(helper.IsNull(dbFilesPathList) && helper.IsNull(creationTimeWiseDbFileList))
                    {
                        return NotFound(new SftpFileDetailsRes(false, StatusCodes.Status404NotFound, ConstantSupplier.DB_DESTINATION_PATH_NOT_FOUND));
                    }

                    var anyNewFilesPathList = downloadedFilesPathList.Except(dbFilesPathList);
                    newFilesPathList = anyNewFilesPathList.ToList();

                    var anyNewCreationTimeWiseInfoList = creationTimeWiseServerFileList.Except(creationTimeWiseDbFileList);
                    newCreationTimeWiseFileInfoList = anyNewCreationTimeWiseInfoList.ToList();

                }
                else
                {
                    newFilesPathList = downloadedFilesPathList;
                }

                
                if (helper.IsNotNull(newFilesPathList) && helper.IsZero(newFilesPathList.Count))
                {
                    return Ok(new SftpFileDetailsRes(true, StatusCodes.Status404NotFound, ConstantSupplier.NO_NEW_FILES));
                }
                List<SftpFileDetails>? entities = null;
                if (helper.IsNotNull(newFilesPathList) && helper.MoreThanZero(newFilesPathList.Count))
                {
                    entities = new();
                    foreach (var item in newFilesPathList)
                    {
                        var serverFile = creationTimeWiseServerFileList.Where(x => x.FileName == Path.GetFileName(item)).FirstOrDefault();
                        var sourcePath = Path.GetFullPath(Path.Join(globalFullServerPath, Path.GetFileName(item)));
                        
                        entities.Add(new SftpFileDetails { Id = Guid.NewGuid(), FileName = Path.GetFileName(item), FileCreationTime = serverFile.FileCreationTime, SourceFilePath = sourcePath, DestinationFilePath = item });
                    }
                }

                var result = await _sftpDataService.AddFilesAsync(entities);
                if (!result.Status)
                {
                    _logService.LogError($"{JsonConvert.SerializeObject(new { result }, Formatting.Indented)}");
                    return Problem(JsonConvert.SerializeObject(new { result }, Formatting.Indented));
                }

                return Ok(result);
            }
            catch (Exception Ex)
            {
                _logService.LogError($"{String.Format(ConstantSupplier.CONTROLLER_ACTION_ERROR_MSG, nameof(downloadsftpfiles), Ex.Message)}");
                return Problem(JsonConvert.SerializeObject(new SftpFileDetailsRes(false, StatusCodes.Status500InternalServerError, Ex.Message),
                Formatting.Indented));
            }
        }

        

    }
}
