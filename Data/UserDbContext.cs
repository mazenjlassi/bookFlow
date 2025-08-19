using Microsoft.EntityFrameworkCore;
using bookFlow.Models;
using bookFlow.Enum;

namespace bookFlow.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Enum conversions
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<Delivery>()
                .Property(d => d.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Loan>()
                .Property(l => l.Status)
                .HasConversion<string>();

            // User - Profile (1:1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserProfile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - Loan (1:N)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Loans)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Ratings (1:N)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Ratings)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Book - Loan (1:N)
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Loans)
                .WithOne(l => l.Book)
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // Book - Ratings (1:N)
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Ratings)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // Loan - Delivery (1:1)
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Delivery)
                .WithOne(d => d.Loan)
                .HasForeignKey<Delivery>(d => d.LoanId)
                .OnDelete(DeleteBehavior.Cascade);

            // User (DeliveryMan role) - Delivery (1:N)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Deliverie)
                .WithOne(d => d.DeliveryMan)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
