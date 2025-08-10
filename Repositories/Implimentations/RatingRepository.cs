using bookFlow.Data;
using bookFlow.Repositories.Implementations;
using bookFlow.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bookFlow.Repositories.Implimentations
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        private readonly UserDbContext _context;

        public RatingRepository(UserDbContext context) : base(context)
        {
            _context = context;
        }
    }
    }
