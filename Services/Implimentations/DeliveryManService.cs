using bookFlow.Enum;
using bookFlow.Models;
using bookFlow.Repositories.Implimentations;
using bookFlow.Repositories.Interfaces;
using bookFlow.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace bookFlow.Services.Implimentations
{
    public class DeliveryManService : IDeliveryManService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public DeliveryManService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>(); // Initialize hasher
        }

        public async Task<User> CreateDeliveryManAsync(CreateDeliveryManDto dto)
        {
            var deliveryMan = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                VehicleNumber = dto.VehicleNumber,
                Role = ERole.DELIVERY_MAN
            };

            // Hash password using PasswordHasher
            deliveryMan.PasswordHash = _passwordHasher.HashPassword(deliveryMan, dto.Password);

            await _userRepository.AddAsync(deliveryMan);
            await _userRepository.SaveChangesAsync();

            return deliveryMan;
        }



        public async Task<IEnumerable<User>> GetAllDeliveryMenAsync()
        {
            var allUsers = await _userRepository.GetAllAsync();

            // Make sure to filter only DeliveryMan role
            var deliveryMen = allUsers
                .Where(u => u.Role == ERole.DELIVERY_MAN)
                .ToList();

            return deliveryMen;
        }


        public Task<User?> GetAllDeliveryMenAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
