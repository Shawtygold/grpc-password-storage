using AuthService.Application.DTO;

namespace AuthService.Application.Abstractions.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> LoginAsync(string login, string password, CancellationToken cancellation = default);
        Task<LoginResponse> LoginWithRefreshTokenAsync(string refreshToken, CancellationToken cancellation = default);
    }
}
