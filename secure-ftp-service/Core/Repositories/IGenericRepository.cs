using System.Linq.Expressions;

namespace secure_ftp_service.Core.Repositories
{
    /// <summary>
    /// It is generic interface which helps to create loosely coupled solution and gives the data related operations interfaces.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? linqExpress = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null);
        Task<T?> GetAsync(int id);
        Task<int> AddRangeAsync(List<T> entities);
        Task<int> AddSinleAsync(T entity);
        Task<T?> FindAsync(Expression<Func<T, bool>> match);

    }
}
