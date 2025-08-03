using bookFlow.Data;
using bookFlow.Models;
using bookFlow.Repositories.Implementations;
using bookFlow.Repositories.Interfaces;

namespace bookFlow.Repositories.Implimentations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext context) : base(context )
        {
            _context = context;
        }
    }
}
