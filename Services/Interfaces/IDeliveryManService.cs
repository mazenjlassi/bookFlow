using bookFlow.Models;

namespace bookFlow.Services.Interfaces
{
    public interface IDeliveryManService
    {
        Task<IEnumerable<DeliveryMan>> GetAllAsync();
        Task<DeliveryMan?> GetByIdAsync(Guid id);
        Task<bool> AddAsync(DeliveryMan deliveryMan, bool isAdmin);
        Task<bool> UpdateAsync(DeliveryMan deliveryMan);
        Task<bool> DeleteAsync(Guid id, bool isAdmin);
    }
}
