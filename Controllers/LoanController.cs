using bookFlow.Enum;
using bookFlow.Models;
using bookFlow.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bookFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        // GET: api/Loan
        [HttpGet("get-all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllLoans()
        {
            var loans = await _loanService.GetAllAsync();
            return Ok(loans);
        }

        // GET: api/Loan/{id}
        [HttpGet("get-by-id/{id}")]
        [Authorize]
        public async Task<IActionResult> GetLoanById(Guid id)
        {
            var loan = await _loanService.GetByIdAsync(id);
            if (loan == null) return NotFound();

            return Ok(loan);
        }

        // POST: api/Loan
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> CreateLoan([FromQuery] string isbn)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized();

            var loan = await _loanService.CreateLoanAsync(isbn, userId);
            if (loan == null) return BadRequest("Book not available or ISBN invalid.");

            return CreatedAtAction(nameof(GetLoanById), new { id = loan.Id }, loan);
        }

        // PUT: api/Loan/{id}/status
        [HttpPut("update/{id}/status")]
        [Authorize]
        public async Task<IActionResult> UpdateLoanStatus(Guid id, [FromQuery] LoanStatus newStatus)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized();

            bool isAdmin = User.IsInRole("Admin");

            var success = await _loanService.UpdateLoanStatusAsync(id, userId, newStatus, isAdmin);
            if (!success) return Forbid("You are not allowed to update this loan status.");

            return NoContent();
        }

        // Helper: Get logged-in user's GUID from claims
        private Guid GetUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userIdString, out var userId) ? userId : Guid.Empty;
        }
    }
}
