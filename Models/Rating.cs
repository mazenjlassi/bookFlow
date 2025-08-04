using bookFlow.Models;

public class Rating
{
    public Guid Id { get; set; }
    public int Score { get; set; } // e.g., 1 to 5
    public string Comment { get; set; }
    public DateTime Date { get; set; }

    public Guid BookId { get; set; }
    public Book Book { get; set; }

    public Guid UserId { get; set; }
    
}
