using AuthService.Application.Abstractions.Repositories;
using AuthService.Domain.Entities;

namespace AuthService.Application.CQRS.Commands.CreateRefreshToken
{
    public class CreateRefreshTokenCommandHandler
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public CreateRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task Handle(CreateRefreshTokenCommand command, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            RefreshToken refreshToken = new(command.Token, command.UserId, command.ExpirationTime);
            await _refreshTokenRepository.AddAsync(refreshToken, cancellation);
        }
    }
}
