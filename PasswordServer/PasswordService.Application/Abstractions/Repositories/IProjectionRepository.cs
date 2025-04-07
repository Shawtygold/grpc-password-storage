using System.Linq.Expressions;

namespace PasswordService.Application.Abstractions.Repositories
{
    // Read side
    public interface IProjectionRepository<TAggregate> where TAggregate : class
    {
        Task<TAggregate?> GetByIdAsync(Guid id);
        Task<TAggregate?> GetByAsync(Expression<Func<TAggregate, bool>> expression);
        Task<IEnumerable<TAggregate>> GetAllAsync(Guid userId);
    }
}
