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
        [HttpPost]
        public async Task<ActionResult<MN_VehicleDto>> Create(MN_CreateVehicleDto dto) {
            try {
                var createdVehicle = await _vehicleService.CreateVehicle(dto);
                // Returns 201 Created with a Location header pointing to the GET endpoint
                return CreatedAtAction(nameof(GetById), new { id = createdVehicle.Id }, createdVehicle);
            } catch (MN_DomainException ex) {
                return BadRequest(new { error = ex.Message });
            }
        }

        // PUT: api/vehicles/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string newStatus) {
            try {
                await _vehicleService.UpdateVehicleStatus(id, newStatus);
                return NoContent();
            } catch (KeyNotFoundException) {
                return NotFound();
            } catch (ArgumentException ex)
              {
                return BadRequest(new { error = ex.Message });
            } catch (MN_DomainException ex)
              {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE: api/vehicles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            await _vehicleService.DeleteVehicle(id);
            return NoContent();
        }
    }
}
