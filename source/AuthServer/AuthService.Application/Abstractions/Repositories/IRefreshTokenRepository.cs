using AuthService.Domain.Entities;
using System.Linq.Expressions;

namespace AuthService.Application.Abstractions.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken, CancellationToken cancellation = default);
        Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellation = default);    
        Task DeleteByAsync(Expression<Func<RefreshToken, bool>> expression, CancellationToken cancellation = default);
        Task<RefreshToken?> GetByTokenAsync(Expression<Func<RefreshToken, bool>> expression, CancellationToken cancellation = default);
    }
}
