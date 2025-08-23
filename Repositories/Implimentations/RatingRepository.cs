using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using bookFlow.Models; 
using bookFlow.Data;
using System.Linq.Expressions;
using bookFlow.Repositories.Interfaces;

namespace BookFlow.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly UserDbContext _context;

        public RatingRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
            return await _context.Ratings.ToListAsync();
        }

        public async Task<Rating> GetByIdAsync(int id)
        {
            return await _context.Ratings.FindAsync(id);
        }

        public async Task<IEnumerable<Rating>> GetByBookIdAsync(Guid bookId)
        {
            return await _context.Ratings
                .Where(r => r.BookId == bookId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Rating>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Ratings
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Rating> AddAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<Rating> UpdateAsync(Rating rating)
        {
            _context.Entry(rating).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null) return false;

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<double> GetAverageRatingForBookAsync(Guid bookId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.BookId == bookId)
                .ToListAsync();

            if (!ratings.Any()) return 0;

            return ratings.Average(r => r.Score);
        }

        public Task<IEnumerable<Rating>> GetByBookIdAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Rating>> GetByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<double> GetAverageRatingForBookAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Rating> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Rating>> FindAsync(Expression<Func<Rating, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<Rating>.AddAsync(Rating entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Rating entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Rating entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
