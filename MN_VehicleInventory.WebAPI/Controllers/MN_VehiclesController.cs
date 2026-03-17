using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MN_VehicleInventory.Application.DTOs;
using MN_VehicleInventory.Application.Services;
using MN_VehicleInventory.Domain.Exceptions;
using MN_VehicleInventory.Application.Interfaces;

namespace MN_VehicleInventory.WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MN_VehiclesController : ControllerBase {
        private readonly MN_IVehicleService _vehicleService;

        // Constructor Injection
        public MN_VehiclesController(MN_IVehicleService vehicleService) {
            _vehicleService = vehicleService;
        }

        // GET: api/vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MN_VehicleDto>>> GetAll() {
            var vehicles = await _vehicleService.GetAllVehicles();
            return Ok(vehicles);
        }

        // GET: api/vehicles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MN_VehicleDto>> GetById(int id) {
            var vehicle = await _vehicleService.GetVehicleById(id);
            if (vehicle == null) {
                return NotFound();
            }
            return Ok(vehicle);
        }

        // POST: api/vehicles
        // Deleted the try and catch logic from create since we have common shared middleware
        [HttpPost]
        public async Task<ActionResult<MN_VehicleDto>> Create(MN_CreateVehicleDto dto) {
            var createdVehicle = await _vehicleService.CreateVehicle(dto);
            // Returns 201 Created with a Location header pointing to the GET endpoint
            return CreatedAtAction(nameof(GetById), new { id = createdVehicle.Id }, createdVehicle);
        }

        // PUT: api/vehicles/{id}/status
        // Deleted the try and catch logic from Update since we have common shared middleware
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string newStatus) {
            await _vehicleService.UpdateVehicleStatus(id, newStatus);
            return NoContent();
        }

        // DELETE: api/vehicles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            await _vehicleService.DeleteVehicle(id);
            return NoContent();
        }
    }
}
