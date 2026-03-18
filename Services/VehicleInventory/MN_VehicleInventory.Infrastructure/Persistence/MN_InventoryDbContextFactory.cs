using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MN_VehicleInventory.Infrastructure.Persistence {
    public class InventoryDbContextFactory : IDesignTimeDbContextFactory<MN_InventoryDbContext> {
        // we need this factory to create the db context at design time for migrations, because the db context is in a different assembly than the startup project
        // after we make it the database connection string is hard coded here, but in a real application we would want to get it from a configuration file or environment variable
        public MN_InventoryDbContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<MN_InventoryDbContext>();

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MN_VehicleInventoryDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new MN_InventoryDbContext(optionsBuilder.Options);
        }
    }
}
