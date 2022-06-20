using Renci.SshNet;

namespace secure_ftp_service.Core.Services
{
    public abstract class RemoteSftpServiceBase
    {
        //public SftpClient SftpClient { get; set; }
        protected SftpClient SftpClient { get; set; }
    }
}