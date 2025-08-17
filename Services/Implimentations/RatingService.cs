using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookFlow.Dto;
using BookFlow.Repositories;

public class RatingService : IRatingService
{
    private readonly IRatingRepository _ratingRepository;

    public RatingService(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public async Task<Rating> AddRatingAsync(RatingDto ratingDto)
    {
        var rating = new Rating
        {
            Id = Guid.NewGuid(),                 // generate new ID
            BookId = ratingDto.BookId,           // set FK
            UserId = ratingDto.UserId,           // set FK
            Score = ratingDto.Score,             // rating value
            Comment = ratingDto.Comment,         // comment text
            Date = DateTime.UtcNow               // timestamp
                                                 // Book and User navigation properties can remain null here
        };

        await _ratingRepository.AddAsync(rating);  // save entity
        return rating;                             // return the saved Rating
    }


    public async Task<Rating> GetRatingByIdAsync(Guid id)
    {
        return await _ratingRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Rating>> GetRatingsByBookIdAsync(Guid bookId)
    {
        var allRatings = await _ratingRepository.GetAllAsync();
        return allRatings.Where(r => r.BookId == bookId);
    }

    public async Task<double> GetAverageRatingByBookIdAsync(Guid bookId)
    {
        // Get all ratings safely, fallback to empty list if null
        var allRatings = await _ratingRepository.GetAllAsync() ?? new List<Rating>();

        // Filter by bookId
        var ratings = allRatings.Where(r => r.BookId == bookId).ToList();

        // If no ratings, return 0
        if (ratings.Count == 0)
            return 0;

        // Return average
        return ratings.Average(r => r.Score);
    }

   
}
