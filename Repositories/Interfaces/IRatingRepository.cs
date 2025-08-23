using System.Collections.Generic;
using System.Threading.Tasks;
using bookFlow.Models;
using bookFlow.Repositories.Implementations;
using bookFlow.Repositories.Interfaces;

namespace BookFlow.Repositories
{
    public interface IRatingRepository :IGenericRepository<Rating>
    {
        Task<IEnumerable<Rating>> GetAllAsync();
        Task<Rating> GetByIdAsync(Guid id);
        Task<IEnumerable<Rating>> GetByBookIdAsync(int bookId);
        Task<IEnumerable<Rating>> GetByUserIdAsync(int userId);
        Task<Rating> AddAsync(Rating rating);
        Task<Rating> UpdateAsync(Rating rating);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<double> GetAverageRatingForBookAsync(int bookId);
    }
}
