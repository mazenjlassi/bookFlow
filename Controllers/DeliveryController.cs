using bookFlow.Models;
using bookFlow.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookFlow.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class DeliveryController : ControllerBase
        {
            private readonly IDeliveryService _deliveryService;

            public DeliveryController(IDeliveryService deliveryService)
            {
                _deliveryService = deliveryService;
            }

            // POST api/delivery
            [HttpPost]
            [Authorize(Roles = "ADMIN")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateDelivery([FromBody] Delivery delivery)
        {
            if (delivery == null)
                return BadRequest("Delivery data is required.");

            delivery.Id = Guid.NewGuid();
            delivery.UserId = null; // Laisser vide pour l’assignation future

            var createdDelivery = await _deliveryService.CreateDeliveryAsync(delivery);
            return Ok(createdDelivery);
        }

        // GET: api/delivery
        [HttpGet]
        [Authorize(Roles = "ADMIN")] // optional, only admins can see all deliveries
        public async Task<IActionResult> GetAllDeliveries()
        {
            var deliveries = await _deliveryService.GetAllAsync();
            return Ok(deliveries);
        }

    }
}
