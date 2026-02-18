using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MN_VehicleInventory.Application.DTOs;

namespace MN_VehicleInventory.Application.Interfaces {
    public interface MN_IVehicleService {
        Task<MN_VehicleDto> CreateVehicle(MN_CreateVehicleDto vDto);
        Task<MN_VehicleDto?> GetVehicleById(int id);
        Task<IEnumerable<MN_VehicleDto>> GetAllVehicles();
        Task UpdateVehicleStatus(int id, string newStatus);
        Task DeleteVehicle(int id);
    }
}
