using secure_ftp_service.Core.Models;
using secure_ftp_service.Core.Repositories;
using secure_ftp_service.Core.Requests;
using secure_ftp_service.Core.Responses;
using System.Linq.Expressions;

namespace secure_ftp_service.Core.Services
{
    //public interface ISftpDataService : IGenericRepository<SftpFileDetails>
    //{
    //    //Task<List<SftpFileDetails>> GetAllAsync();
    //    //Task<SftpFileDetails?> GetAsync(int id);
    //    //Task<int> AddRangeAsync(List<SftpFileDetails> entities);
    //    //Task<SftpFileDetails> AddSinleAsync(SftpFileDetails entity);
    //    //Task<SftpFileDetails?> FindAsync(Expression<Func<SftpFileDetails, bool>> match);
    //}
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
