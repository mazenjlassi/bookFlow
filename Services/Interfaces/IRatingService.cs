using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bookFlow.Dto;

public interface IRatingService
{
    Task<Rating> AddRatingAsync(RatingDto ratingDto);
    Task<Rating> GetRatingByIdAsync(Guid id);
    Task<IEnumerable<Rating>> GetRatingsByBookIdAsync(Guid bookId);
    Task<double> GetAverageRatingByBookIdAsync(Guid bookId);
    Task<bool> DeleteRatingAsync(Guid id);

}
