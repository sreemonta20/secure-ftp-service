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
            List<string> serverFilesList = new();
            List<string> dbFilesList =  new();
            List<string> newDownloadedFilesList = new();
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
                    serverFilesList = helper.GetFiles(globalFullServerPath);
                }

                if (helper.IsNotNull(serverFilesList) && helper.MoreThanZero(serverFilesList.Count))
                {
                    var isDownload = await _sftpService.DownloadFiles(localPath, serverPath, serverFilesList);
                    if (!isDownload)
                    {
                        return BadRequest(new SftpFileDetailsRes(false, StatusCodes.Status400BadRequest, ConstantSupplier.DOWNLOAD_FAILED_MSG));
                    }
                }
                else
                {
                    return NotFound(new SftpFileDetailsRes(false, StatusCodes.Status404NotFound, ConstantSupplier.NO_FILES_FOUND_SERVER_PATH));
                    //return Problem(JsonConvert.SerializeObject(new SftpFileDetailsRes(false, StatusCodes.Status404NotFound, ConstantSupplier.NO_FILES_FOUND_SERVER_PATH)));
                }


                //var combinedLocalPath = Path.GetFullPath(localPath);
                var downloadedFileList = helper.GetFiles(localPath.Trim('\''));
                if (helper.IsNull(downloadedFileList) && helper.IsZero(downloadedFileList.Count))
                {
                    //return Problem(JsonConvert.SerializeObject(new SftpFileDetailsRes(false, StatusCodes.Status404NotFound, ConstantSupplier.DOWNLOAD_FILES_NOT_FOUND_MSG),
                    //Formatting.Indented));
                    return NotFound(new SftpFileDetailsRes(false, StatusCodes.Status404NotFound, ConstantSupplier.DOWNLOAD_FILES_NOT_FOUND_MSG));
                }

                var allDbStoredFiles = await _sftpDataService.GetAllFilesAsync();
                if(helper.IsNotNull(allDbStoredFiles.ListData) && helper.MoreThanZero(allDbStoredFiles.ListData.Count))
                {
                    foreach (var item in allDbStoredFiles.ListData)
                    {
                        dbFilesList.Add(item.DestinationFilePath);
                    }

                    if(helper.IsNull(dbFilesList))
                    {
                        //    return Problem(JsonConvert.SerializeObject(new SftpFileDetailsRes(false, StatusCodes.Status404NotFound, ConstantSupplier.DB_DESTINATION_PATH_NOT_FOUND),
                        //Formatting.Indented));
                        return NotFound(new SftpFileDetailsRes(false, StatusCodes.Status404NotFound, ConstantSupplier.DB_DESTINATION_PATH_NOT_FOUND));
                    }

                    //newDownloadedFilesList = (List<string>)downloadedFileList.Except(dbFilesList);
                    var anyNewList = downloadedFileList.Except(dbFilesList);
                    newDownloadedFilesList = anyNewList.ToList();

                }
                else
                {
                    newDownloadedFilesList = downloadedFileList;
                }

                if(helper.IsNull(newDownloadedFilesList) && helper.IsZero(newDownloadedFilesList.Count))
                {
                    return Ok(new SftpFileDetailsRes(true, StatusCodes.Status404NotFound, ConstantSupplier.NO_NEW_FILES));
                }
                List<SftpFileDetails>? entities = null;
                if (helper.IsNotNull(newDownloadedFilesList) && helper.MoreThanZero(newDownloadedFilesList.Count))
                {
                    entities = new();
                    foreach (var item in newDownloadedFilesList)
                    {
                        var sourcePath = Path.GetFullPath(Path.Join(globalFullServerPath, Path.GetFileName(item)));
                        entities.Add(new SftpFileDetails { Id = Guid.NewGuid(), FileName = Path.GetFileName(item), FileCreationime = DateTime.UtcNow, SourceFilePath = sourcePath, DestinationFilePath = item });
                    }
                }

                var result = await _sftpDataService.AddFilesAsync(entities);
                if (!result.Status)
                {
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
