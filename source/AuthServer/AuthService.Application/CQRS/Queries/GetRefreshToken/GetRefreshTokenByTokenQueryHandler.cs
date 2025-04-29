using AuthService.Application.Abstractions.Repositories;
using AuthService.Domain.Entities;

namespace AuthService.Application.CQRS.Queries.GetRefreshToken
{
    public class GetRefreshTokenByTokenQueryHandler
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public GetRefreshTokenByTokenQueryHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken?> Handle(GetRefreshTokenByTokenQuery query, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            return await _refreshTokenRepository.GetByTokenAsync(t => t.Token == query.Token, cancellation);
        }
    }
}
