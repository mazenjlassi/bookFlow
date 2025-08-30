using bookFlow.Models;
using bookFlow.Repositories.Implementations;

namespace bookFlow.Repositories.Interfaces
{
    public interface IDeliveryRepository : IGenericRepository<Delivery>
    {
        Task AddAsync(Delivery delivery);
        Task<Delivery?> GetByIdAsync(Guid id);
        Task<IEnumerable<Delivery>> GetAllAsync();

        Task<IEnumerable<Delivery>> GetAllPendingAsync();
        Task<IEnumerable<Delivery>> GetAllByDeliveryManIdAsync(Guid deliveryManId);

    }
}