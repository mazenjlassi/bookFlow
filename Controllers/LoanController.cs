using bookFlow.Dto;
using bookFlow.Enum;
using bookFlow.Models;
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
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanDto request)
        {
            var loanDto = await _loanService.CreateLoanAsync(request.BookId, request.UserId);
            if (loanDto == null)
                return BadRequest("Book is not available or invalid book/user ID.");

            return CreatedAtAction(nameof(GetLoanById), new { id = loanDto.Id }, loanDto);
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
    }
    }
