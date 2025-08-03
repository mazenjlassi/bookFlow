using bookFlow.Enum;

namespace bookFlow.Models
{
    public class Delivery
    {
        
            public int Id { get; set; }

            public int LoanId { get; set; }
            public Loan Loan { get; set; }

            public DeliveryStatus Status { get; set; }
            public DateTime? DeliveryDate { get; set; }
        }

}

