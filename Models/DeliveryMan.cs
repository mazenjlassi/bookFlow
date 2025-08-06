using System.ComponentModel.DataAnnotations.Schema;

namespace bookFlow.Models
{
    [Table("DeliveryMen")]
    public class DeliveryMan : User
    {
        public string? VehicleNumber { get; set; }
        public string? PhoneNumber { get; set; }

        // Relationship: One delivery man can have many deliveries
        public ICollection<Delivery> Deliveries { get; set; }
    }
}
