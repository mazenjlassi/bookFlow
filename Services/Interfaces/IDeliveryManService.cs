using bookFlow.Models;

namespace bookFlow.Services.Interfaces
{
    public interface IDeliveryManService
    {
        Task<User> CreateDeliveryManAsync(CreateDeliveryManDto dto);
        Task<IEnumerable<User>> GetAllDeliveryMenAsync();
        Task<User?> GetAllDeliveryMenAsync(Guid id);
    }
}
