using secure_ftp_service.Core.Models;

namespace secure_ftp_service.Core.Services
{
    /// <summary>
    /// It is an interface which helps to create loosely coupled solution and gives the privilege to work with sftp server.<br/>
    /// </summary>
    
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
        //Task<bool> DownloadFiles(string localFilePath, string remoteFilePath, List<string> serverFiles);
        Task<bool> DownloadFilesAsync(string localFilePath, string remoteFilePath);
        string ServerDetails();
        Task<List<ServerFileInfo>> getFileInfo(string path);

    }
}
