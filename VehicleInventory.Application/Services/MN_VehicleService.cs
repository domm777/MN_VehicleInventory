using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Entities;
using VehicleInventory.Domain.Enums;

namespace VehicleInventory.Application.Services {
    public class MN_VehicleService : MN_IVehicleService {
        private readonly MN_IVehicleRepository _vehicleRepository;

        // Constructor injection of the repository
        public async Task<MN_VehicleDto> CreateVehicle(MN_VehicleDto dto) {
            var vehicle = new MN_Vehicle(dto.VehicleCode, dto.LocationId, dto.VehicleType);

            await _vehicleRepository.AddVehicle(vehicle);
            await _vehicleRepository.SaveChanges();
            return MapToDto(vehicle);
        }

        // We can add more methods for retrieving, updating, and deleting vehicles as needed
        public async Task<IEnumerable<MN_VehicleDto>> GetAllVehicles() {
            var vehicles = await _vehicleRepository.GetAllVehicles();
            return vehicles.Select(MapToDto);
        }

        // Example method to get a vehicle by its ID
        public async Task<MN_VehicleDto?> GetVehicleById(Guid id) {
            var vehicle = await _vehicleRepository.GetVehicleById(id);
            if (vehicle == null) return null;
            return MapToDto(vehicle);
        }

        // Method to update a vehicle's status
        public async Task UpdateVehicleStatus(Guid id, string newStatus) {
            var vehicle = await _vehicleRepository.GetVehicleById(id);
            if (vehicle == null) throw new Exception($"Vehicle with Id: {id} not found");

            if (!Enum.TryParse<MN_VehicleStatus>(newStatus, true, out var statusEnum))
                throw new Exception($"Invalid status value: {newStatus}");

            // we can implement more complex status transition logic here so that we don't allow invalid transitions
            switch (statusEnum) {
                case MN_VehicleStatus.Available:
                    vehicle.MarkAvailable();
                    break;
                case MN_VehicleStatus.Rented:
                    vehicle.MarkRented();
                    break;
                case MN_VehicleStatus.Reserved:
                    vehicle.MarkReserved();
                    break;
                case MN_VehicleStatus.UnderService:
                    vehicle.MarkServiced();
                    break;
                default:
                    throw new ArgumentException("Unsupported status transition.");
            }

            await _vehicleRepository.UpdateVehicle(vehicle);
            await _vehicleRepository.SaveChanges();
        }

        // Method to delete a vehicle by its ID
        public async Task DeleteVehicle(Guid id) {
            await _vehicleRepository.DeleteVehicle(id);
            await _vehicleRepository.SaveChanges();
        }

        // Helper method to map MN_Vehicle entity to MN_VehicleDto
        private MN_VehicleDto MapToDto(MN_Vehicle newVehicle) {
            return new MN_VehicleDto {
                Id = newVehicle.Id,
                VehicleCode = newVehicle.VehicleCode,
                LocationId = newVehicle.LocationId,
                VehicleType = newVehicle.VehicleType,
                Status = newVehicle.Status.ToString()
            };
        }
    }
}
