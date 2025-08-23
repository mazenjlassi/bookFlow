using bookFlow.Dto;
using bookFlow.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RatingController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    // POST api/rating
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddRating([FromBody] RatingDto ratingDto)
    {
        if (ratingDto == null || ratingDto.UserId == Guid.Empty || ratingDto.BookId == Guid.Empty)
            return BadRequest("Invalid rating data.");

        var createdRating = await _ratingService.AddRatingAsync(ratingDto);
        return CreatedAtAction(nameof(GetRatingById), new { id = createdRating.Id }, createdRating);
    }

    // GET api/rating/{id}
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetRatingById(Guid id)
    {
        var rating = await _ratingService.GetRatingByIdAsync(id);
        if (rating == null)
            return NotFound();

        return Ok(rating);
    }


    // DELETE api/rating/{id}
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "ADMIN")] // only admin can delete ratings
    public async Task<IActionResult> DeleteRating(Guid id)
    {
        var deleted = await _ratingService.DeleteRatingAsync(id);
        if (!deleted)
            return NotFound("Rating not found.");

        return Ok("Rating deleted successfully.");
    }

    // GET api/rating/book/{bookId}
    [HttpGet("book/{bookId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetRatingsByBook(Guid bookId)
    {
        var ratings = await _ratingService.GetRatingsByBookIdAsync(bookId);
        return Ok(ratings);
    }

    // GET api/rating/book/{bookId}/average
    [HttpGet("book/{bookId:guid}/average")]
    [Authorize]
    public async Task<IActionResult> GetAverageRatingByBook(Guid bookId)
    {
        var avg = await _ratingService.GetAverageRatingByBookIdAsync(bookId);
        return Ok(avg);
    }
}
