using System.ComponentModel.DataAnnotations;
using bookFlow.Enum;

namespace bookFlow.Models
{
    public class Delivery
    {
        [Key]
        public Guid Id { get; set; }

        public Guid LoanId { get; set; }
        public Loan Loan { get; set; }

        public DeliveryStatus Status { get; set; }
        public DateTime? DeliveryDate { get; set; }
        


        public Guid? UserId { get; set; }
        public User? DeliveryMan { get; set; }
    }
}
