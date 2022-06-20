using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secure_ftp_service.Core.Models
{
    public class SftpFileDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DateTime FileCreationime { get; set; } = DateTime.UtcNow;
        public string SourceFilePath { get; set; } = string.Empty;
        public string DestinationFilePath { get; set; } = string.Empty;


        //public SftpFileDetails(Guid Id, string FileName, DateTime FileCreationime, string SourceFilePath, string DestinationFilePath)
        //{
        //    this.Id = Id;
        //    this.FileName = FileName;
        //    this.FileCreationime = FileCreationime;
        //    this.SourceFilePath = SourceFilePath;
        //    this.DestinationFilePath = DestinationFilePath;
        //}
    }
}
