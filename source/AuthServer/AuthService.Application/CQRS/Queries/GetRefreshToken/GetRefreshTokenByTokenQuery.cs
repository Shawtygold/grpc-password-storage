using AuthService.Domain.Entities;
using System.Linq.Expressions;

namespace AuthService.Application.CQRS.Queries.GetRefreshToken
{
    public record GetRefreshTokenByTokenQuery(string Token);
}
