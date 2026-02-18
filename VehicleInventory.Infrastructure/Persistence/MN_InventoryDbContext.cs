using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Entities;

namespace VehicleInventory.Infrastructure.Persistence {
    public class MN_InventoryDbContext : DbContext {
        public MN_InventoryDbContext(DbContextOptions options) : base(options) {}

        public DbSet<MN_Vehicle> Vehicles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            modelBuilder.Entity<MN_Vehicle>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.VehicleCode).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LocationId).IsRequired();
                entity.Property(e => e.VehicleType).HasConversion<string>();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
