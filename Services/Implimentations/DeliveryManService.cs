using bookFlow.Models;
using bookFlow.Repositories.Interfaces;
using bookFlow.Services.Interfaces;

namespace bookFlow.Services.Implementations
{
    public class DeliveryManService : IDeliveryManService
    {
        private readonly IDeliveryManRepository _repo;

        public DeliveryManService(IDeliveryManRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<DeliveryMan>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<DeliveryMan?> GetByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<bool> AddAsync(DeliveryMan deliveryMan, bool isAdmin)
        {
            if (!isAdmin) return false;

            await _repo.AddAsync(deliveryMan);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(DeliveryMan deliveryMan)
        {
            _repo.Update(deliveryMan);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, bool isAdmin)
        {
            if (!isAdmin) return false;

            var deliveryMan = await _repo.GetByIdAsync(id);
            if (deliveryMan == null) return false;

            _repo.Delete(deliveryMan);
            return await _repo.SaveChangesAsync();
        }
    }
}
