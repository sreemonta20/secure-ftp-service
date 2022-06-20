using Renci.SshNet;
using Renci.SshNet.Async;
using secure_ftp_service.Core.Models;
using secure_ftp_service.Helpers;
using ConnectionInfo = Renci.SshNet.ConnectionInfo;

namespace secure_ftp_service.Core.Services
{
    /// <summary>
    /// It is generic class which implements all the methods defined in the <see  cref="IRemoteSftpService"/>
    /// </summary>
    public class RemoteSftpService : RemoteSftpServiceBase, IRemoteSftpService
    {
        /// <summary>
        /// Declaration & Initialization
        /// </summary>
        private readonly ILogService _logService;
        private string _serverDetails;

        /// <summary>
        /// Constructor initialization
        /// </summary>
        public RemoteSftpService(ILogService logService)
        {
            this._logService = logService;
        }

        public void serviceInitialize(RemoteSftpConfigModel settingModel)
        {
            try
            {
                AssistantHelper oHelper = new();
                _serverDetails = oHelper.ServerDetails(settingModel.Host, settingModel.Port.ToString(), settingModel.UserName, settingModel.Type);
                var connectionInfo = new ConnectionInfo(settingModel.Host, settingModel.Port, settingModel.UserName, new PasswordAuthenticationMethod(settingModel.UserName, settingModel.Password));
                SftpClient = new SftpClient(connectionInfo);
            }
            catch (Exception Ex)
            {
                throw new Exception($"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
                ///throw;
                ///
                //_plugin.LogError(Ex, Ex.Message);

                //// Return the Response object.
                //return new() { Success = false, Message = Ex.Message, MessageType = EnumResponseType.Error, Result = false, ResponseCode = (int)HttpStatusCode.InternalServerError };
            }
            
        }

        public bool IsConnected()
        {
            return SftpClient.IsConnected;
        }

        public void Connect()
        {
            try
            {
                SftpClient.Connect();
            }
            catch (Exception Ex)
            {
                throw new Exception($"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
            }
        }

        public void Disconnect()
        {
            try
            {
                SftpClient.Disconnect();
            }
            catch (Exception Ex)
            {
                throw new Exception($"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
            }
        }

        public bool FileExists(string filePath)
        {
            return SftpClient.Exists(filePath);
        }

        public bool DirectoryExists(string directoryPath)
        {
            return SftpClient.Exists(directoryPath);
        }

        public void CreateDirectoryIfNotExists(string directoryPath)
        {
            try
            {
                if (!DirectoryExists(directoryPath))
                {
                    SftpClient.CreateDirectory(directoryPath);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception($"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
            }
        }

        public void SetWorkingDirectory(string directoryPath)
        {
            SftpClient.ChangeDirectory(directoryPath);
        }

        public void SetRootAsWorkingDirectory()
        {
            SetWorkingDirectory("");
        }

        public async Task<bool> DownloadFile(string localFilePath, string remoteFilePath)
        {
            try
            {
                using (Stream fileStream = File.Create(localFilePath))
                {
                    await SftpClient.DownloadAsync(remoteFilePath, fileStream);
                    return true;
                }
                
            }
            catch (Exception Ex)
            {
                throw new Exception($"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
            }
        }

        public async Task<bool> DownloadFilesAsync(string localFilePath, string remoteFilePath)
        {
            var files = SftpClient.ListDirectory(remoteFilePath);
            try
            {
                foreach (var file in files)
                {
                    string remoteFileName = file.Name;
                    if ((!file.Name.StartsWith(".")) && (!file.Name.EndsWith(".")))
                    {

                        var combinedLocalPath = Path.Join(localFilePath.Trim('\''), file.Name);
                        var combinedServerPath = Path.Join(remoteFilePath, file.Name);
                        using (Stream fileStream = File.Create(combinedLocalPath))
                        {
                            //await Task.Delay(0);
                            await SftpClient.DownloadAsync(combinedServerPath, fileStream);
                        }
                    }
                }
                return true;
            }
            catch (Exception Ex)
            {
                throw new Exception($"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
            }
            
        }

        public string ServerDetails()
        {
            return _serverDetails;
        }

        public async Task<List<ServerFileInfo>> getFileInfo(string path)
        {
            List<ServerFileInfo> creationTimeWiseServerFileList = new();
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
            foreach (FileInfo file in files)
            {
                await Task.Delay(0);
                creationTimeWiseServerFileList.Add(new ServerFileInfo { FileName = file.Name, FileCreationTime = file.CreationTime });
            }
            return creationTimeWiseServerFileList;
        }

        public void Dispose()
        {
            if (SftpClient != null)
            {
                SftpClient.Dispose();
            }
        }
    }
}
