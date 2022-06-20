using secure_ftp_service.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secure_ftp_service.Core.Requests
{
    /// <summary>
    /// This class carries the server, services, and other responses.
    /// </summary>
    public class SftpFileDetailsReq
    {
        public string FileName { get; set; } = string.Empty;
        public DateTime FileCreationTime { get; set; } = DateTime.UtcNow;
        public string SourceFilePath { get; set; } = string.Empty;
        public string DestinationFilePath { get; set; } = string.Empty;
    }
}
