using bookFlow.Enum;

namespace bookFlow.Models
{
    public class Loan
    {
       
            public int Id { get; set; }

            public int BookId { get; set; }
            public Book Book { get; set; }

            public string UserId { get; set; }
            public User User { get; set; }

            public DateTime StartDate { get; set; }
            public DateTime? ReturnDate { get; set; }

            public LoanStatus Status { get; set; }

            public Delivery Delivery { get; set; }
        }

    }

