using System.ComponentModel.DataAnnotations;

namespace bookFlow.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string PublishedDate { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<Loan> Loans { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
