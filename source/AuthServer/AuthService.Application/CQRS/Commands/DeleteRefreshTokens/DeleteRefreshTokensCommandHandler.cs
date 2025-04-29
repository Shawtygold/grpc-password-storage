using AuthService.Application.Abstractions.Repositories;

namespace AuthService.Application.CQRS.Commands.DeleteRefreshTokens
{
    public sealed class DeleteRefreshTokensCommandHandler
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public DeleteRefreshTokensCommandHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }


        public async Task Handle(DeleteRefreshTokensCommand command, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await _refreshTokenRepository.DeleteByAsync(r => r.UserId == command.UserId, cancellation);
        }
    }
}
