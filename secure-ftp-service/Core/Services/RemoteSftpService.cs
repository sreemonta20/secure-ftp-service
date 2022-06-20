using Renci.SshNet;
using Renci.SshNet.Async;
using secure_ftp_service.Core.Models;
using secure_ftp_service.Helpers;
using ConnectionInfo = Renci.SshNet.ConnectionInfo;

namespace secure_ftp_service.Core.Services
{
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

        public async Task<bool> DownloadFiles(string localFilePath, string remoteFilePath, List<string> serverFiles)
        {
            var files = SftpClient.ListDirectory(remoteFilePath);
            try
            {
                //var combinedServerPath = Path.GetFullPath(Path.Join(@"C:\rebex_tiny_sftp_server\data", remoteFilePath));
                foreach (var file in files)
                {
                    string remoteFileName = file.Name;
                    if ((!file.Name.StartsWith(".")) && (!file.Name.EndsWith(".")))
                    {

                        var combinedLocalPath = Path.Join(localFilePath.Trim('\''), file.Name);
                        var combinedServerPath = Path.Join(remoteFilePath, file.Name);
                        using (Stream fileStream = File.Create(combinedLocalPath))
                        {
                            await Task.Delay(0);
                            SftpClient.DownloadFile(combinedServerPath, fileStream);
                            //return true;
                        }
                    }
                }
                return true;
            }
            catch (Exception Ex)
            {
                throw new Exception($"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
            }
            //var files = SftpClient.ListDirectory(remoteFilePath);
            //try
            //{
            //    foreach (var file in files)
            //    {
            //        string remoteFileName = file.Name;
            //        if ((!file.Name.StartsWith(".")) && (!file.Name.EndsWith(".")))
            //        {
            //            var combinedPath = Path.Join(@localFilePath, file.Name);
            //            using (Stream fileStream = File.Create(Path.Join(@localFilePath, file.Name)))
            //            {
            //                await SftpClient.DownloadAsync(@"" + remoteFilePath, fileStream);
            //            }
            //        }
            //    }
            //    return true;
            //}
            //catch (Exception Ex)
            //{
            //    throw new Exception($"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
            //}

            //try
            //{
            //    foreach (var file in serverFiles)
            //    {

            //        var combinedPath = @""+localFilePath.ToString() +"\\"+Path.GetFileName(file).ToString();

            //        using (Stream fileStream = File.Create(combinedPath))
            //        {
            //            await SftpClient.DownloadAsync(@"" + remoteFilePath, fileStream);
            //        }
            //    }
            //    return true;
            //}
            //catch (Exception Ex)
            //{
            //    throw new Exception($"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
            //}
        }

        public string ServerDetails()
        {
            return _serverDetails;
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
