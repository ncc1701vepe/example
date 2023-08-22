using Km56.Infrastructure.ReusableDesign;
using Km56.MyStore.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Km56.MyStore.Infrastructure.Repository
{
    public class ItemRepository : Domain.Service.IItemRepository
    {
        private IDataMapper<Domain.Entity.Item> _itemDataMapper;

        public ItemRepository(IDbContextFactory<Sql.MyStoreContext> dbContextFactory)
        {
            _itemDataMapper = new ItemDataMapper(dbContextFactory.CreateDbContext());
        }

        public async Task<IEnumerable<Domain.Entity.Item>> GetAllAsync()
        {
            return await _itemDataMapper.GetAllAsync();
        }

        public async Task<Domain.Entity.Item?> GetByIdAsync(int id)
        {
            return await _itemDataMapper.GetByIdAsync(id);
        }
    }
}
