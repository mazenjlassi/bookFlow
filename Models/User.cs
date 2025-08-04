using System.ComponentModel.DataAnnotations;
using bookFlow.Enum;

namespace bookFlow.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
      
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public ERole Role { get; set; } = ERole.USER;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }



        public ICollection<Loan> Loans { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
