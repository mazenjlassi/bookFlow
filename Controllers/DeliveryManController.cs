using bookFlow.Enum;
using bookFlow.Models;
using bookFlow.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace bookFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryManController : ControllerBase
    {
        private readonly IDeliveryManService _deliveryManService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public DeliveryManController(IDeliveryManService deliveryManService, IPasswordHasher<User> passwordHasher)
        {
            _deliveryManService = deliveryManService; // DI properly assigned
            _passwordHasher = passwordHasher;
        }

        // POST api/deliveryman
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateDeliveryMan([FromBody] CreateDeliveryManDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _deliveryManService.CreateDeliveryManAsync(dto);
            return Ok(created);
        }


        [HttpGet("all")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllDeliveryMen()
        {
            var deliveryMen = await _deliveryManService.GetAllDeliveryMenAsync();
            return Ok(deliveryMen);
        }
    }
}
