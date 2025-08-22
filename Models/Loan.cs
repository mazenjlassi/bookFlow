using System.ComponentModel.DataAnnotations;
using bookFlow.Enum;

namespace bookFlow.Models
{
    public class Loan
    {
        [Key]
        public Guid Id { get; set; }

            public Guid BookId { get; set; }
            public Book Book { get; set; }

            public Guid UserId { get; set; }
            public User User { get; set; }

            public DateTime StartDate { get; set; }
            public DateTime ReturnDate { get; set; }

            public LoanStatus Status { get; set; }

            public Delivery Delivery { get; set; }
        }

    }

