using AutoMapper;
using secure_ftp_service.Core.Models;
using secure_ftp_service.Core.Repositories;
using secure_ftp_service.Core.Requests;
using secure_ftp_service.Core.Responses;
using secure_ftp_service.Helpers;
using System.Linq.Expressions;

namespace secure_ftp_service.Core.Services
{
    /// <summary>
    /// It is generic class which implements all the methods defined in the <see  cref="ISftpDataService"/>
    /// </summary>
    public class SftpDataService : ISftpDataService
    {
        private readonly ILogService _logService;
        private readonly IGenericRepository<SftpFileDetails> _dataRepo;
        private readonly IMapper _mapper;

        public SftpDataService(ILogService logService, IGenericRepository<SftpFileDetails> dataRepo, IMapper mapper)
        {
            _logService = logService;
            _dataRepo = dataRepo;
            _mapper = mapper;
        }

        public async Task<SftpFileDetailsRes> GetAllFilesAsync()
        {
            AssistantHelper helper;
            try
            {
                helper = new();
                var listdata = await this._dataRepo.GetAllAsync();
                if (helper.IsNull(listdata))
                {
                    return new SftpFileDetailsRes(false, StatusCodes.Status404NotFound, ConstantSupplier.LIST_SFTP_DATA_LIST_RETRIVE_FAILED_MSG);
                }
                return new SftpFileDetailsRes(true, StatusCodes.Status200OK, ConstantSupplier.LIST_SFTP_DATA_LIST_RETRIVE_SUCCESS_MSG, listdata.ToList());
            }
            catch (Exception Ex)
            {
                return  new SftpFileDetailsRes(false, StatusCodes.Status500InternalServerError, $"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
            }
        }

        public async Task<SftpFileDetailsRes?> GetFileAsync(int id)
        {
            AssistantHelper helper;
            try
            {
                helper = new();
                var data = await this._dataRepo.GetAsync(id);
                if (helper.IsNotNull(data))
                {
                    return new SftpFileDetailsRes(false, StatusCodes.Status404NotFound, ConstantSupplier.LIST_SFTP_DATA_RETRIVE_FAILED_MSG);
                }
                return new SftpFileDetailsRes(true, StatusCodes.Status200OK, ConstantSupplier.LIST_SFTP_DATA_RETRIVE_SUCCESS_MSG, data);
            }
            catch (Exception Ex)
            {
                return new SftpFileDetailsRes(false, StatusCodes.Status500InternalServerError, $"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
            }
        }

        public async Task<SftpFileDetailsRes> AddFilesAsync(List<SftpFileDetails> entities)
        {
            AssistantHelper helper;
            try
            {
                helper = new();
                var state = await this._dataRepo.AddRangeAsync(entities);
                if (!helper.MoreThanZero(state))
                {
                    return new SftpFileDetailsRes(false, StatusCodes.Status400BadRequest, ConstantSupplier.SFTP_DATA_SAVE_FAILED_MSG);
                }
                return new SftpFileDetailsRes(true, StatusCodes.Status200OK, ConstantSupplier.SFTP_DATA_SAVE_SUCCESS_MSG, state);
            }
            catch (Exception Ex)
            {
                return new SftpFileDetailsRes(false, StatusCodes.Status500InternalServerError, $"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
            }
        }
        //public async Task<SftpFileDetailsRes> AddFilesAsync(List<SftpFileDetailsReq> saveList)
        //{
        //    AssistantHelper helper;
        //    try
        //    {
        //        helper = new();
        //        List<SftpFileDetails> entities = _mapper.Map<List<SftpFileDetails>>(saveList);
        //        var state = await this._dataRepo.AddRangeAsync(entities);
        //        if (!helper.MoreThanZero(state))
        //        {
        //            return new SftpFileDetailsRes(false, StatusCodes.Status400BadRequest, ConstantSupplier.SFTP_DATA_SAVE_FAILED_MSG);
        //        }
        //        return new SftpFileDetailsRes(true, StatusCodes.Status200OK, ConstantSupplier.SFTP_DATA_SAVE_SUCCESS_MSG, state);
        //    }
        //    catch (Exception Ex)
        //    {
        //        return new SftpFileDetailsRes(false, StatusCodes.Status500InternalServerError, $"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
        //    }
        //}

        public async Task<SftpFileDetailsRes> AddSinleFileAsync(SftpFileDetails entity)
        {
            AssistantHelper helper;
            try
            {
                helper = new();
                var state = await this._dataRepo.AddSinleAsync(entity);
                if (!helper.MoreThanZero(state))
                {
                    return new SftpFileDetailsRes(false, StatusCodes.Status400BadRequest, ConstantSupplier.SFTP_DATA_SAVE_FAILED_MSG);
                }
                return new SftpFileDetailsRes(true, StatusCodes.Status200OK, ConstantSupplier.SFTP_DATA_SAVE_SUCCESS_MSG, state);
            }
            catch (Exception Ex)
            {
                return new SftpFileDetailsRes(false, StatusCodes.Status500InternalServerError, $"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
            }
        }

        public async Task<SftpFileDetailsRes?> FindFileAsync(Expression<Func<SftpFileDetails, bool>> match)
        {
            AssistantHelper helper;
            try
            {
                helper = new();
                var data = await this._dataRepo.FindAsync(match);
                if (helper.IsNull(data))
                {
                    return new SftpFileDetailsRes(false, StatusCodes.Status404NotFound, ConstantSupplier.SFTP_DATA_FOUND_FAILED_MSG);
                }
                return new SftpFileDetailsRes(true, StatusCodes.Status200OK, ConstantSupplier.SFTP_DATA_FOUND_SUCCESS_MSG, data);
            }
            catch (Exception Ex)
            {
                return new SftpFileDetailsRes(false, StatusCodes.Status500InternalServerError, $"{ConstantSupplier.EXCEPTION_MSG}{Ex.Message}{ConstantSupplier.NEW_LINE}{ConstantSupplier.EXCEPTION_INNER_MSG}{Ex.InnerException}.");
            }
        }
        
    }
}
