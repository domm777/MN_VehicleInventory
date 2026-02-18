using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Application.DTOs;

namespace VehicleInventory.Application.Interfaces {
    public interface MN_IVehicleService {
        Task<MN_VehicleDto> CreateVehicle(MN_VehicleDto vehicle);
        Task<MN_VehicleDto?> GetVehicleById(Guid id);
        Task<IEnumerable<MN_VehicleDto>> GetAllVehicles();
        Task UpdateVehicleStatus(Guid id, string newStatus);
        Task DeleteVehicle(Guid id);
    }
}
