using Newtonsoft.Json;
using secure_ftp_service.Core.Models;

namespace secure_ftp_service.Core.Responses
{
    /// <summary>
    /// It carries the service response of sftp service operation.
    /// </summary>
    public class SftpFileDetailsRes
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<SftpFileDetails>? ListData { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SftpFileDetails? Data { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? State { get; set; }

        public SftpFileDetailsRes(bool status, int statuscode, string message, List<SftpFileDetails>? listData)
        {
            this.Status = status;
            this.StatusCode = statuscode;
            this.Message = message;
            this.ListData = listData;
        }

        public SftpFileDetailsRes(bool status, int statuscode, string message, SftpFileDetails? data)
        {
            this.Status = status;
            this.StatusCode = statuscode;
            this.Message = message;
            this.Data = data;
        }

        public SftpFileDetailsRes(bool status, int statuscode, string message, int? state)
        {
            this.Status = status;
            this.StatusCode = statuscode;
            this.Message = message;
            this.State = state;
        }
        public SftpFileDetailsRes(bool status, int statuscode, string message)
        {
            this.Status = status;
            this.StatusCode = statuscode;
            this.Message = message;
        }
        public SftpFileDetailsRes(string message)
        {
            this.Message = message;
        }
    }
}
