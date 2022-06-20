using Renci.SshNet;

namespace secure_ftp_service.Core.Services
{
    /// <summary>
    /// Abstract class, which defines the common property and it is inherited by the single subclass RemoteSftpService. 
    /// </summary>
    public abstract class RemoteSftpServiceBase
    {
        protected SftpClient SftpClient { get; set; }
    }
}