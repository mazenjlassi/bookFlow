using bookFlow.Models;
using bookFlow.Repositories.Implimentations;
using bookFlow.Repositories.Interfaces;
using bookFlow.Services.Interfaces;

namespace bookFlow.Services.Implimentations
{
  
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
      public async Task<bool> CreateAsync(User user)
        {
            await _userRepository.AddAsync(user);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(User user)
        {
             _userRepository.Delete(user);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _userRepository.Update(user);
            return await _userRepository.SaveChangesAsync();
        }
    }
}
