using AuthService.Application.Abstractions.Providers;
using AuthService.Application.Abstractions.Security;
using AuthService.Application.Abstractions.Services;
using AuthService.Application.CQRS.Commands.CreateRefreshToken;
using AuthService.Application.CQRS.Queries.GetRefreshToken;
using AuthService.Application.CQRS.Queries.GetUserByLogin;
using AuthService.Application.DTO;
using AuthService.Domain.Entities;
using AuthService.Domain.Exceptions;
using Microsoft.Extensions.Options;
using Wolverine;

namespace AuthService.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IMessageBus _messageBus;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenProvider _refreshTokenProvider;
        private readonly IOptions<RefreshTokenSettings> _refreshTokenSettings;

        public LoginService(IMessageBus messageBus, IPasswordHasher passwordHasher, IRefreshTokenProvider refreshTokenProvider, IOptions<RefreshTokenSettings> refreshTokenSettings)
        {
            _messageBus = messageBus;
            _passwordHasher = passwordHasher;
            _refreshTokenProvider = refreshTokenProvider;
            _refreshTokenSettings = refreshTokenSettings;
        }

        public async Task<LoginResponse> LoginAsync(string login, string password, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            GetUserByLoginQuery query = new(login);
            UserDTO user = await _messageBus.InvokeAsync<UserDTO>(query, cancellation);

            if (!_passwordHasher.Verify(password, user.PasswordHash))
                throw new AuthenticationException();

            string accessToken = _refreshTokenProvider.GetAccessToken(user.Id);
            string refreshToken = _refreshTokenProvider.GenerateRefreshToken();
            DateTime expiredTime = DateTime.UtcNow.AddDays(int.Parse(_refreshTokenSettings.Value.LifetimeInDays));

            CreateRefreshTokenCommand command = new(refreshToken, user.Id, expiredTime);
            await _messageBus.InvokeAsync(command, cancellation);

            return new LoginResponse(accessToken, refreshToken);           
        }

        public async Task<LoginResponse> LoginWithRefreshTokenAsync(string refreshToken, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            GetRefreshTokenByTokenQuery query = new(refreshToken);
            var dbRefreshToken = await _messageBus.InvokeAsync<RefreshToken>(query, cancellation);

            if(dbRefreshToken is null || dbRefreshToken.ExpirationTime < DateTime.UtcNow)
                throw new RefreshTokenExpiredException();

            string newAccessToken = _refreshTokenProvider.GetAccessToken(dbRefreshToken.UserId);
            string newRefreshToken = _refreshTokenProvider.GenerateRefreshToken();
            DateTime expiredTime = DateTime.UtcNow.AddDays(int.Parse(_refreshTokenSettings.Value.LifetimeInDays));

            CreateRefreshTokenCommand command = new(newRefreshToken, dbRefreshToken.UserId, expiredTime);
            await _messageBus.InvokeAsync(command, cancellation);

            return new LoginResponse(newAccessToken, newRefreshToken);
        }
    }
}
