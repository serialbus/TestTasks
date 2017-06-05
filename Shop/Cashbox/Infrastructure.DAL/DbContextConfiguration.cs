using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL
{
    public class DbContextConfiguration: DbConfiguration
    {
        public DbContextConfiguration()
        {
            this.SetDatabaseInitializer<ShopContext>(new DatabaseInitializer());
            this.SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
        }
    }
}
