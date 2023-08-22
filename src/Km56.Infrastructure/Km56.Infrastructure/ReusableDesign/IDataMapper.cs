namespace Km56.Infrastructure.ReusableDesign
{
    /// <summary>
    /// The contract for the Data Mapper
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDataMapper<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get the entity identified by the provided Id
        /// </summary>
        /// <param name="id">The identifier of the entity</param>
        /// <returns>The entity that matches the Id, if any</returns>
        Task<TEntity?> GetByIdAsync(int id);

        /// <summary>
        /// Gets all the data of target repository
        /// </summary>
        /// <returns>All the entities in the data store</returns>
        Task<List<TEntity>> GetAllAsync();
    }
}
