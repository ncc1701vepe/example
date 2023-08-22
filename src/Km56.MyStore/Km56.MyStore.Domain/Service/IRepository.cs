namespace Km56.MyStore.Domain.Service
{
    public interface IRepository<TEntity>
        where TEntity : Entity.EntityBase
    {
        Task<TEntity?> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
