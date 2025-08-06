using bookFlow.Data;
using bookFlow.Models;
using bookFlow.Repositories.Interfaces;

namespace bookFlow.Repositories.Implementations
{
    public class DeliveryManRepository : GenericRepository<DeliveryMan>, IDeliveryManRepository
    {
        public DeliveryManRepository(UserDbContext context) : base(context)
        {
        }
            
    }
}
