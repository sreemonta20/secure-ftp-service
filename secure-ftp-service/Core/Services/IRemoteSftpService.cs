using secure_ftp_service.Core.Models;

namespace secure_ftp_service.Core.Services
{
    public interface IRemoteSftpService: IDisposable
    {
        void serviceInitialize(RemoteSftpConfigModel settingModel);
        bool IsConnected();
        void Connect();
        void Disconnect();
        bool FileExists(string filePath);
        bool DirectoryExists(string directoryPath);
        void CreateDirectoryIfNotExists(string directoryPath);
        void SetWorkingDirectory(string path);
        void SetRootAsWorkingDirectory();
        Task<bool> DownloadFile(string localFilePath, string remoteFilePath);
        Task<bool> DownloadFiles(string localFilePath, string remoteFilePath, List<string> serverFiles);
        string ServerDetails();

    }
}
