using System.ComponentModel.DataAnnotations;

namespace MN_VehicleInventory.Infrastructure.Persistence {
    public class MN_VehicleLocation {
        [Required]
        public int LocatioinID { get; set; }
        [Required]
        public string Address { get; set; }
        public MN_VehicleLocation(int locatioinID, string address) {
            LocatioinID = locatioinID;
            Address = address;
        }
    }
}