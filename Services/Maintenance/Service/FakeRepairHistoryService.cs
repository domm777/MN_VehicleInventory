using Maintenance.Model;
using Maintenance.Repository;

namespace Maintenance.Service {
    public class FakeRepairHistoryService : IRepairHistoryService {
        public RepairHistoryDto AddRepair(RepairHistoryDto repair) {
            repair.Id = new Random().Next(1000, 9999);
            return repair;
        }

        public List<RepairHistoryDto> GetByVehicleId(int vehicleId) {
            return new List<RepairHistoryDto>
            {
                new RepairHistoryDto
                {
                    Id = 1,
                    VehicleId = vehicleId,
                    RepairDate = DateTime.Now.AddDays(-10),
                    Description = "Oil change",
                    Cost = 89.99m,
                    PerformedBy = "Quick Lube"
                },
                new RepairHistoryDto
                {
                    Id = 2,
                    VehicleId = vehicleId,
                    RepairDate = DateTime.Now.AddDays(-40),
                    Description = "Brake pad replacement",
                    Cost = 350.00m,
                    PerformedBy = "Auto Repair Pro"
                }
            };
        }
    }
}

//namespace Maintenance.Service {
//    public class FakeRepairHistoryService : IRepairHistoryService {
//        // In-memory store for fake repairs
//        private readonly List<RepairHistoryDto> _repairs;

//        public FakeRepairHistoryService() {
//            _repairs = new List<RepairHistoryDto>
//            {
//                new RepairHistoryDto
//                {
//                    Id = 1,
//                    VehicleId = 1,
//                    RepairDate = DateTime.Now.AddDays(-10),
//                    Description = "Oil change",
//                    Cost = 89.99m,
//                    PerformedBy = "Quick Lube"
//                },
//                new RepairHistoryDto
//                {
//                    Id = 2,
//                    VehicleId = 1,
//                    RepairDate = DateTime.Now.AddDays(-40),
//                    Description = "Brake pad replacement",
//                    Cost = 350.00m,
//                    PerformedBy = "Auto Repair Pro"
//                }
//            };
//        }
//        // Might need to come back to this later to ensure it generates unique IDs and handles edge cases, but for now it should suffice for testing purposes
//        public RepairHistoryDto AddRepair(RepairHistoryDto repair) {
//            // Add to in-memory store so GetByVehicleId can return it later
//            _repairs.Add(repair);
//            var rnd = Random.Shared;
//            int candidate;

//            do {
//                candidate = rnd.Next(1000, 9999);
//            } while (_repairs.Any(r => r.Id == candidate));

//            repair.Id = candidate;
//            repair.RepairDate = DateTime.Now;

//            _repairs.Add(repair);

//            return repair;
//        }

//        public List<RepairHistoryDto> GetByVehicleId(int vehicleId) {
//            // Return repairs for the requested vehicle from the same in-memory store
//            return _repairs.Where(r => r.VehicleId == vehicleId).ToList();
//        }
//    }
//}
