using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Entities;

namespace VehicleInventory.Application.Interfaces {

    public interface MN_IVehicleRepository {
        Task<MN_Vehicle?> GetVehicleById(Guid Id);
        Task<IEnumerable<MN_Vehicle>> GetAllVehicles();
        Task AddVehicle(MN_Vehicle vehicle);
        Task UpdateVehicle(MN_Vehicle vehicle);
        Task DeleteVehicle(Guid id);
        Task SaveChanges();
    }
}
