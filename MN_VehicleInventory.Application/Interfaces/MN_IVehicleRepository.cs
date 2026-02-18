using MN_VehicleInventory.Domain.Entities;

namespace MN_VehicleInventory.Application.Interfaces {

    public interface MN_IVehicleRepository {
        Task<MN_Vehicle?> GetVehicleById(int Id);
        Task<IEnumerable<MN_Vehicle>> GetAllVehicles();
        Task AddVehicle(MN_Vehicle vehicle);
        Task UpdateVehicle(MN_Vehicle vehicle);
        Task DeleteVehicle(int id);
        Task SaveChanges();
    }
}
