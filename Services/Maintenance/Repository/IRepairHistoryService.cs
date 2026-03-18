using Maintenance.Model;

namespace Maintenance.Repository {
    public interface IRepairHistoryService {
        List<RepairHistoryDto> GetByVehicleId(int vehicleId);
        RepairHistoryDto AddRepair(RepairHistoryDto repair);
    }
}
