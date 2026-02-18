using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MN_VehicleInventory.Domain.Entities;
using MN_VehicleInventory.Infrastructure.Persistence;
using MN_VehicleInventory.Application.Interfaces;

namespace MN_VehicleInventory.Infrastructure.Repositories {
    public class MN_VehicleRepository : MN_IVehicleRepository {
        private readonly MN_InventoryDbContext _context;

        public MN_VehicleRepository(MN_InventoryDbContext context) {
            _context = context;
        }
        public async Task<IEnumerable<MN_Vehicle>> GetAllVehicles() {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<MN_Vehicle?> GetVehicleById(int Id) {
            return await _context.Vehicles.FindAsync(Id);
        }

        public async Task AddVehicle(MN_Vehicle vehicle) {
            await _context.Vehicles.AddAsync(vehicle);
        }

        public Task UpdateVehicle(MN_Vehicle vehicle) {
            _context.Vehicles.Update(vehicle);
            return Task.CompletedTask;
        }

        public Task DeleteVehicle(int id) {
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle != null) {
                _context.Vehicles.Remove(vehicle);
            }
            return Task.CompletedTask;
        }

        public async Task SaveChanges() {
            await _context.SaveChangesAsync();
        }
    }
}
