using bookFlow.Models;
using bookFlow.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet("get-all")]
        
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var books = await _bookService.GetAllAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all books");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var book = await _bookService.GetByIdAsync(id);
                if (book == null)
                    return NotFound("Book not found");

                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching book by ID");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            try
            {
                var Book = await _bookService.CreateAsync(book);
                return Ok(Book);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"General error: {ex.Message}");
            }
        }


        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var book = await _bookService.GetByIdAsync(id);
                if (book == null)
                    return NotFound("Book not found");

                var result = await _bookService.DeleteAsync(book);
                if (result)
                    return Ok("Book deleted successfully");

                return StatusCode(500, "Failed to delete book");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting book");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("update-availability/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAvailability(Guid id)
        {
            try
            {
                var updated = await _bookService.UpdateAvailabilityAsync(id);
                if (!updated)
                    return NotFound("Book not found or update failed");

                return Ok("Book availability toggled");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating availability for book with ID {Id}", id);
                return StatusCode(500, ex.Message);
            }
        }

       



    }
}
