using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.Domain.Entities {
    public class MN_Vehicle {
        // Constructor which initializes the Id property with a new Guid
        // Guid is a unique identifier that can be used to uniquely identify each vehicle in the inventory
        public Guid Id { get; private set; }

        // VehicleCode is a unique code that identifies the vehicle, it is required and cannot be null or whitespace
        public string VehicleCode { get; private set; }
        // LocationId is an integer that represents the location of the vehicle, required and cannot be null or whitespace
        public int LocationId { get; private set; }
        // VehicleType is a string that represents the type of the vehicle, required and cannot be null or whitespace
        public string VehicleType { get; private set; }
        // Status is an enumeration that represents the current status of the vehicle, it is required and cannot be null
        public MN_VehicleStatus Status { get; private set; }

        // Constructor for the MN_Vehicle class which takes in the vehicle code, location id, vehicle type, and status as parameters
        public MN_Vehicle(string vehicleCode, int locationId, string vehicleType) {
            if (string.IsNullOrWhiteSpace(vehicleCode))
                throw new MN_DomainException("Vehicle Code is required.");

            if (string.IsNullOrWhiteSpace(vehicleType))
                throw new MN_DomainException("Vehicle Type is required.");

            Id = Guid.NewGuid();
            VehicleCode = vehicleCode;
            LocationId = locationId;
            VehicleType = vehicleType;
            Status = MN_VehicleStatus.Available;
        }

        // Domain Behavior : Method to update the status of the vehicle
        public void MarkRented() {
            if (Status == MN_VehicleStatus.Rented)
                throw new MN_DomainException("Vehicle is already rented.");

            if (Status == MN_VehicleStatus.UnderService)
                throw new MN_DomainException("Vehicle is under service and cannot be rented.");

            if (Status == MN_VehicleStatus.Reserved)
                throw new MN_DomainException("Vehicle is reserved and cannot be rented. ReCheck reservation");

            Status = MN_VehicleStatus.Rented;
        }

        // if the vehicle is already rented, under service or reserved, it cannot be reserved again and an exception will be thrown
        public void MarkReserved() {
            if (Status == MN_VehicleStatus.Rented)
                throw new MN_DomainException("Vehicle is already rented and cannot be reserved.");
            if (Status == MN_VehicleStatus.UnderService)
                throw new MN_DomainException("Vehicle is under service and cannot be reserved.");
            if (Status == MN_VehicleStatus.Reserved)
                throw new MN_DomainException("Vehicle is already reserved.");
            Status = MN_VehicleStatus.Reserved;
        }

        public void MarkServiced() {
            if (Status == MN_VehicleStatus.Rented)
                throw new MN_DomainException("Vehicle is currently rented and cannot be marked as under service.");
            if (Status == MN_VehicleStatus.Reserved)
                throw new MN_DomainException("Vehicle is reserved and cannot be marked as under service.");
            if (Status == MN_VehicleStatus.UnderService)
                throw new MN_DomainException("Vehicle is already under service.");
            Status = MN_VehicleStatus.UnderService;
        }

        public void MarkAvailable() {
            if (Status == MN_VehicleStatus.Rented)
                throw new MN_DomainException("Vehicle is currently rented and cannot be marked as available.");
            if (Status == MN_VehicleStatus.Reserved)
                throw new MN_DomainException("Vehicle is reserved and cannot be marked as available.");
            if (Status == MN_VehicleStatus.UnderService)
                throw new MN_DomainException("Vehicle is under service and cannot be marked as available.");
            Status = MN_VehicleStatus.Available;
        }

        public void ReleaseReservation() {
            if (Status != MN_VehicleStatus.Reserved)
                throw new MN_DomainException("Vehicle is not currently reserved and cannot release reservation.");
            Status = MN_VehicleStatus.Available;
        }

        public void MarkReturned() {
            if (Status != MN_VehicleStatus.Rented)
                throw new MN_DomainException("Vehicle is not currently rented and cannot be returned.");
            Status = MN_VehicleStatus.Available;
        }
    }
}
