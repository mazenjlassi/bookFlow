using bookFlow.Dto;
using bookFlow.Enum;
using bookFlow.Models;
using bookFlow.Services.Implimentations;
using bookFlow.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bookFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService, IDeliveryService deliveryService)
        {
            _loanService = loanService;
            _deliveryService = deliveryService;
        }

        [HttpGet("get-all")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllLoans()
        {
            var loans = await _loanService.GetAllAsync();
            return Ok(loans);
        }

        [HttpPost("add")]
        
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanDto createLoanDto)
        {
            if (createLoanDto == null || createLoanDto.BookId == Guid.Empty || createLoanDto.UserId == Guid.Empty)
                return BadRequest("Invalid data.");

            var loanDto = await _loanService.CreateLoanAsync(createLoanDto.BookId, createLoanDto.UserId);

            if (loanDto == null)
                return BadRequest("Book not available or user not found.");

            return Ok(loanDto);
        }



        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetLoanById(Guid id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);
            if (loan == null)
                return NotFound();

            // Map to DTO if needed, or create a separate method for DTO mapping
            return Ok(loan);
        }


        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteLoan(Guid id)
        {
            var result = await _loanService.DeleteLoanIfEnCoursAsync(id);

            if (!result)
                return BadRequest("Loan not found or status is not EN_COURS");

            return NoContent();
        }

        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetLoansByUser(Guid userId)
        {
            var loans = await _loanService.GetAllLoansByUserIdAsync(userId);
            if (loans == null)
                return NotFound("No loans found for this user.");

            return Ok(loans);
        }

        // GET api/loan/book/{bookId}
        [HttpGet("book/{bookId:guid}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetLoansByBookId(Guid bookId)
        {
            var loans = await _loanService.GetLoansByBookIdAsync(bookId);
            if (loans == null || !loans.Any())
                return NotFound("No loans found for this book.");

            return Ok(loans);
        }

        [HttpPut("approve/{loanId:guid}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ApproveLoan(Guid loanId)
        {
            var loan = await _loanService.ApproveLoanAsync(loanId);
            if (loan == null) return NotFound("Loan not found.");

            var delivery = new Delivery
            {
                Id = Guid.NewGuid(),
                LoanId = loan.Id,
                Status = DeliveryStatus.EN_ATTENTE,
                DeliveryDate = DateTime.Now,
                UserId = null, // ✅ no delivery-man at creation
                Loan = loan
            };

            await _deliveryService.CreateDeliveryAsync(delivery);

            return Ok(new { Loan = loan, Message = "Loan approved successfully" });
        }

    }
}
