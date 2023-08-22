using Km56.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Km56.MyStore.Infrastructure.DataAccess
{
    public class ItemDataMapper : DataMapperSql<Domain.Entity.Item, Sql.Item>
    {
        public ItemDataMapper(DbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
