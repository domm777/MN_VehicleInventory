using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Infrastructure.Persistence {
    public class InventoryDbContextFactory : IDesignTimeDbContextFactory<MN_InventoryDbContext> {
        public MN_InventoryDbContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<MN_InventoryDbContext>();

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MN_VehicleInventoryDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new MN_InventoryDbContext(optionsBuilder.Options);
        }
    }
}
