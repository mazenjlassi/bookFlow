using System;
using bookFlow.Data;
using bookFlow.Enum;
using bookFlow.Models;
using bookFlow.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("get-pending")]
        public async Task<IActionResult> GetPending()
        {
            var deliveries = await _deliveryService.GetPendingDeliveriesAsync();
            return Ok(deliveries);
        }

        [HttpGet("get-by-deliveryman/{deliveryManId}")]
        public async Task<IActionResult> GetByDeliveryMan(Guid deliveryManId)
        {
            var deliveries = await _deliveryService.GetDeliveriesByDeliveryManIdAsync(deliveryManId);
            return Ok(deliveries);
        }

        [HttpPost("assign/{deliveryId}/{deliveryManId}")]
        public async Task<IActionResult> Assign(Guid deliveryId, Guid deliveryManId)
        {
            var updated = await _deliveryService.AssignDeliveryAsync(deliveryId, deliveryManId);
            
            return Ok(updated);
        }

        [HttpPut("update-status/{deliveryId}")]
        public async Task<IActionResult> UpdateStatus(Guid deliveryId, [FromBody] DeliveryStatus status)
        {
            var updated = await _deliveryService.UpdateStatusAsync(deliveryId, status);
            return Ok(updated);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var delivery = await _deliveryService.GetAllAsync();
            var found = delivery.FirstOrDefault(d => d.Id == id);
            if (found == null)
                return NotFound();

            return Ok(found);
        }
        [HttpPost("return-by-loan/{loanId}")]
        public async Task<IActionResult> ReturnBookByLoan(Guid loanId)
        {
            // Reuse GetDeliveryByLoanIdAsync
            var delivery = await _deliveryService.GetDeliveryByLoanIdAsync(loanId);

            if (delivery == null)
                return NotFound(new { message = "No delivery found for this loan" });

            // Update Loan status
            if (delivery.Loan != null)
            {
                delivery.Loan.Status = LoanStatus.RETOURNE;
            }

            // Make book available again
            if (delivery.Loan?.Book != null)
            {
                delivery.Loan.Book.IsAvailable = true;
            }

            // Update Delivery status through your existing service
            await _deliveryService.UpdateStatusAsync(delivery.Id, DeliveryStatus.LIVRE);

            // Build return object
            var result = new
            {
                delivery.Id,
                Loan = new
                {
                    delivery.Loan.Id,
                    delivery.Loan.Book.Title,
                    delivery.Loan.Status
                },
                DeliveryMan = delivery.DeliveryMan != null ? new
                {
                    delivery.DeliveryMan.Id,
                    delivery.DeliveryMan.Email,
                    delivery.DeliveryMan.PhoneNumber
                } : null
            };

            return Ok(result);
        }



        [HttpGet("get-by-loan/{loanId}")]
        public async Task<IActionResult> GetDeliveryByLoanId(Guid loanId)
        {
            var delivery = await _deliveryService.GetDeliveryByLoanIdAsync(loanId);
            if (delivery == null)
                return NotFound(new { message = "No delivery found for this loan" });

            return Ok(delivery);
        }




    }
}
