using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MN_VehicleInventory.Application.DTOs {
    // we add data annotations to this dto because of webApi will send 400 status code error
    // if the user POSTS empty string so we show the exact error message.
    public class MN_CreateVehicleDto {
        [Required(ErrorMessage = "Vehicle Code is required.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Vehicle Code must be between 2 - 10 characters.")]
        public string VehicleCode { get; set; }

        [Required(ErrorMessage = "Location ID is required.")]
        [Range(1, 1000, ErrorMessage = "Location ID must be vaild positive number.")]
        public int LocationId { get; set; }

        [StringLength(30, ErrorMessage = "Vehicle Type cannot exceed 30 characters.")]
        public string VehicleType { get; set; }
    }
}
