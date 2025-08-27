using bookFlow.Models;

namespace bookFlow.Services.Interfaces
{
    public interface IDeliveryService
    {
        Task<Delivery> CreateDeliveryAsync(Delivery delivery);
        Task<Delivery?> GetDeliveryByIdAsync(Guid id);
        Task<IEnumerable<Delivery>> GetAllAsync();

    }
}
