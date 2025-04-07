using AuthService.Domain.Entities;
using System.Linq.Expressions;

namespace AuthService.Application.Abstractions.Repositories
{
    public interface IUserRepository :
     IDisposable
    {
        public Task AddAsync(User user);
        public Task<User?> GetByAsync(Expression<Func<User, bool>> expression);
    }
}
