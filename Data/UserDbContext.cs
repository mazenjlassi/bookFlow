using bookFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace bookFlow.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<UserProfile> UserProfiles{ get; set; }
    }
}
