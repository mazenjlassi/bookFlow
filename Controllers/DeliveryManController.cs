using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bookFlow.Models;
using bookFlow.Services.Interfaces;
using System.Security.Claims;

namespace bookFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryManController : ControllerBase
    {
        private readonly IDeliveryManService _deliveryManService;

        public DeliveryManController(IDeliveryManService deliveryManService)
        {
            _deliveryManService = deliveryManService;
        }

        // GET: api/DeliveryMan
        [HttpGet("get-all")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAll()
        {
            var deliveryMen = await _deliveryManService.GetAllAsync();
            return Ok(deliveryMen);
        }

        // GET: api/DeliveryMan/{id}
        [HttpGet("get-by-id/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var deliveryMan = await _deliveryManService.GetByIdAsync(id);
            if (deliveryMan == null) return NotFound();
            return Ok(deliveryMan);
        }

        // POST: api/DeliveryMan
        [HttpPost("add")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Add([FromBody] DeliveryMan deliveryMan)
        {
            var isAdmin = User.IsInRole("ADMIN");
            var result = await _deliveryManService.AddAsync(deliveryMan, isAdmin);
            if (!result) return Forbid("Only admins can add delivery men.");
            return Ok(deliveryMan);
        }

        // PUT: api/DeliveryMan
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] DeliveryMan deliveryMan)
        {
            var result = await _deliveryManService.UpdateAsync(deliveryMan);
            if (!result) return NotFound("Delivery man not found.");
            return Ok(deliveryMan);
        }

        // DELETE: api/DeliveryMan/{id}
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isAdmin = User.IsInRole("ADMIN");
            var result = await _deliveryManService.DeleteAsync(id, isAdmin);
            if (!result) return Forbid("Only admins can delete delivery men.");
            return Ok("Deleted successfully.");
        }
    }
}
