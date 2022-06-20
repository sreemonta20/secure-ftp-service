using secure_ftp_service.Core.Models;
using secure_ftp_service.Core.Repositories;
using secure_ftp_service.Core.Requests;
using secure_ftp_service.Core.Responses;
using System.Linq.Expressions;

namespace secure_ftp_service.Core.Services
{
    /// <summary>
    /// It is an interface which helps to create loosely coupled solution and gives the privilege to work with database.<br/>
    /// </summary>
    public interface ISftpDataService
    {
        Task<SftpFileDetailsRes> GetAllFilesAsync();
        Task<SftpFileDetailsRes?> GetFileAsync(int id);
        Task<SftpFileDetailsRes> AddFilesAsync(List<SftpFileDetails> entities);
        //Task<SftpFileDetailsRes> AddFilesAsync(List<SftpFileDetailsReq> entities);
        Task<SftpFileDetailsRes> AddSinleFileAsync(SftpFileDetails entity);
        Task<SftpFileDetailsRes?> FindFileAsync(Expression<Func<SftpFileDetails, bool>> match);
    }
}
