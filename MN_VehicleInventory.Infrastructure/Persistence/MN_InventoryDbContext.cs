using Microsoft.EntityFrameworkCore;
using MN_VehicleInventory.Domain.Entities;
using MN_VehicleInventory.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MN_VehicleInventory.Infrastructure.Persistence {
    public class MN_InventoryDbContext : DbContext {
        public MN_InventoryDbContext(DbContextOptions options) : base(options) {}

        public DbSet<MN_Vehicle> Vehicles { get; set; }
        public DbSet<MN_VehicleLocation> VehicleLocations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            modelBuilder.Entity<MN_Vehicle>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.VehicleCode)
                .HasConversion(
                    v => v.Value,
                    v => new VehicleCode(v)).IsRequired().HasMaxLength(50);
                
                entity.Property(e => e.VehicleType)
                .HasConversion(
                    v => v.Value, 
                    v => new VehicleType(v)).IsRequired();

                entity.Property(e => e.Status).IsRequired();
            });
            modelBuilder.Entity<MN_VehicleLocation>(entity => {
                entity.HasKey(e => e.LocatioinID);
                entity.Property(e => e.Address).IsRequired();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
