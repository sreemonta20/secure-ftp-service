using System.Linq.Expressions;

namespace secure_ftp_service.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? linqExpress = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null);
        Task<T?> GetAsync(int id);
        Task<int> AddRangeAsync(List<T> entities);
        Task<int> AddSinleAsync(T entity);
        Task<T?> FindAsync(Expression<Func<T, bool>> match);

    }
}
