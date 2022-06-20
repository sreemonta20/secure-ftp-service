namespace secure_ftp_service.Core.Models
{
    /// <summary>
    /// It is a helper class to read the server and database record's filename and file creation time.
    /// </summary>
    public class ServerFileInfo
    {
        public string FileName { get; set; } = string.Empty;
        public DateTime FileCreationTime { get; set; }
    }
}
