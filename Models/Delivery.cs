using bookFlow.Enum;

namespace bookFlow.Models
{
    public class Delivery
    {
        public Guid Id { get; set; }

        public Guid LoanId { get; set; }
        public Loan Loan { get; set; }

        public Guid? DeliveryManId { get; set; }
        public DeliveryMan? DeliveryMan { get; set; }

        public DeliveryStatus Status { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
