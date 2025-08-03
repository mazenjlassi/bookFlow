using bookFlow.Dto;
using bookFlow.Models;

namespace bookFlow.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(RegistryDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
    }
}
