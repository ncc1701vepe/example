using Km56.Infrastructure.Common;
using Km56.Infrastructure.ReusableDesign;
using Microsoft.EntityFrameworkCore;

namespace Km56.Infrastructure.DataAccess
{
    /// <summary>
    /// Contract for a bidirectional transfer of data between the persistent data store and the in-memory data representation   
    /// </summary>
    /// <typeparam name="TEntity">The Domain Entity representing the in-memory data representation</typeparam>
    /// <typeparam name="TDbRecord">The persistent data store entity</typeparam>
    public abstract class DataMapperSql<TEntity, TDbRecord> : IDataMapper<TEntity> 
        where TEntity : class
        where TDbRecord : class
    {
        protected DbContext? _dbContext = null;
        protected DbSet<TDbRecord>? _dbSet = null;

        protected ObjectMapper _mapper;

        public DataMapperSql(DbContext dbContext) 
        {
            if (dbContext is not null)
            {
                _dbContext = dbContext;
                _dbSet = dbContext.Set<TDbRecord>();
            }

            _mapper = new ObjectMapper();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            if (_dbSet is null) return null;

            TDbRecord? dbResult = await _dbSet.FindAsync(id);

            if (dbResult is null) return null;

            return ToEntity(dbResult);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            if (_dbSet is null) return new List<TEntity>();
            List<TDbRecord> resultList = await _dbSet.ToListAsync();

            return resultList.Select(r => ToEntity(r)).ToList();
        }

        protected virtual TEntity ToEntity(TDbRecord entity)
        {
            return _mapper.Map<TDbRecord, TEntity>(entity);
        }

        protected virtual TDbRecord ToDbEntity(TEntity dto)
        {
            return _mapper.Map<TEntity, TDbRecord>(dto);
        }
    }
}
