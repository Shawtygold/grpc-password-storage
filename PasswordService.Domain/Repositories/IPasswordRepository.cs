using PasswordService.Domain.Entities;
using System.Linq.Expressions;

namespace PasswordService.Domain.Repositories
{
    public interface IPasswordRepository :
        IDisposable
    {
        Task AddAsync(Password entity);
        Task UpdateAsync(Password entity);
        Task DeleteAsync(int entityId);
        Task<Password?> GetByAsync(Expression<Func<Password, bool>> expression);
        Task<Password?> GetByIDAsync(int entityId);
        Task<IEnumerable<Password>> GetCollectionByAsync(Expression<Func<Password, bool>> expression);
    }
}
