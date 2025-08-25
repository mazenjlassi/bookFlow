using bookFlow.Models;
using bookFlow.Repositories.Implementations;

namespace bookFlow.Repositories.Interfaces
{
    public interface IDeliveryRepository: IGenericRepository<Delivery>
    {
        Task<Delivery> CreateAsync(Delivery delivery);
        Task<Delivery?> GetByIdAsync(Guid id);
        Task SaveChangesAsync();
    }
}
