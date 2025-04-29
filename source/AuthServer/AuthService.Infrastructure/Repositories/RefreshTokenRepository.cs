using AuthService.Application.Abstractions.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.AppContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthService.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationContext _dbContext;

        public RefreshTokenRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellation);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            _dbContext.RefreshTokens.Update(refreshToken);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<RefreshToken?> GetByTokenAsync(Expression<Func<RefreshToken, bool>> expression, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            return await _dbContext.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(expression, cancellation);
        }

        public async Task DeleteByAsync(Expression<Func<RefreshToken, bool>> expression, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await _dbContext.RefreshTokens.Where(expression).ExecuteDeleteAsync(cancellation);
        }
    }
}
