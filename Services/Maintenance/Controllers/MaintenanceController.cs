using Maintenance.Model;
using Maintenance.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.Controllers {
    [ApiController]
    [Route("api/maintenance")]
    public class MaintenanceController : ControllerBase {
        private readonly IRepairHistoryService _service;
        private readonly Dictionary<string, int> _usageCounts;
        public MaintenanceController(IRepairHistoryService service, Dictionary<string, int> usageCounts) {
            _service = service;
            _usageCounts = usageCounts;
        }
        [HttpGet("vehicles/{vehicleId}/repairs")]
        public IActionResult GetRepairHistory(int vehicleId) {
            var history = _service.GetByVehicleId(vehicleId);
            return Ok(history);
        }
        // Endpoint to add a new repair record for a vehicle
        [HttpPost]
        public IActionResult AddRepair([FromBody] RepairHistoryDto repair) {
            // Validate the input data and return appropriate error responses if the data is invalid
            if (repair.VehicleId <= 0) {
                return BadRequest(new {
                    error = "InvalidParameter",
                    message = "VehicleId must be greater than zero."
                });
            }
            // if the description is null or empty, return a 400 Bad Request with a custom error message
            if (string.IsNullOrWhiteSpace(repair.Description)) {
                return BadRequest(new {
                    error = "InvalidParameter",
                    message = "Description must not be empty."
                });
            }
            // if the cost is negative, return a 400 Bad Request with a custom error message
            if (repair.Cost < 0) {
                return BadRequest(new {
                    error = "InvalidParameter",
                    message = "Cost cannot be negative."
                });
            }

            var created = _service.AddRepair(repair);
            // Return a 201 Created response with the location of the newly created resource
            return CreatedAtAction(
                nameof(GetRepairHistory),
                new { vehicleId = created.VehicleId },
                created
            );
        }
        // Endpoint to simulate a server error for testing error handling
        // This will cause a division by zero exception
        [HttpGet("crash")]
        public IActionResult Crash() {
            int x = 0;
            int y = 5 / x;
            return Ok();
        }
        // Endpoint to track API usage
        // And return the number of calls made by each client (identified by an API key in the header)
        [HttpGet("usage")]
        public IActionResult Usage() {
            var key = Request.Headers["X-Api-Key"].ToString();

            if (!_usageCounts.ContainsKey(key))
                _usageCounts[key] = 0;

            _usageCounts[key]++;

            return Ok(new {
                clientId = key,
                callCount = _usageCounts[key]
            });
        }
    }
}
