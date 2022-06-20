using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using secure_ftp_service.Helpers;
using secure_ftp_service.Persistence.ORM;
using System.Linq.Expressions;

namespace secure_ftp_service.Core.Repositories
{
    /// <summary>
    /// It is generic class which implements all the methods defined in the <see  cref="IGenericRepository{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SFTPDbContext _context;
        private readonly DbSet<T> _dbCollection;

        public GenericRepository(SFTPDbContext context)
        {
            _context = context;
            _dbCollection = _context.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? linqExpress = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null)
        {
            AssistantHelper oHelper = new();
            IQueryable<T> query = _dbCollection;
            if (oHelper.IsNotNull(linqExpress))
            {
                query = query.Where(linqExpress);
            }

            if (oHelper.IsNotNull(includes))
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }

            if (oHelper.IsNotNull(orderBy))
            {
                query = orderBy(query);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            var item = await _dbCollection.FindAsync(id);
            return item;
        }

        public async Task<int> AddRangeAsync(List<T> entities)
        {
            await _dbCollection.AddRangeAsync(entities);
            int state = await _context.SaveChangesAsync();
            return state;
            //await _context.BulkInsertAsync(entities);
            //int state = await _context.SaveChangesAsync();
            //return state;
        }

        public async Task<int> AddSinleAsync(T entity)
        {
            await _dbCollection.AddAsync(entity);
            int state = await _context.SaveChangesAsync();
            return state;
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> match)
        {
            var item =  await _dbCollection.SingleOrDefaultAsync<T>(match);
            
            return item;
        }

    }
}
