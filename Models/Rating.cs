using System.ComponentModel.DataAnnotations;
using bookFlow.Models;

public class Rating
{
    [Key]
    public Guid Id { get; set; }
    public int Score { get; set; } // e.g., 1 to 5
    public string Comment { get; set; }
    public DateTime Date { get; set; }

    public Guid BookId { get; set; }
    public Book Book { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
    
}
