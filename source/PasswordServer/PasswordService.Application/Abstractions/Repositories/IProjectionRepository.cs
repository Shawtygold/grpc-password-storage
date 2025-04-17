using System.Linq.Expressions;

namespace PasswordService.Application.Abstractions.Repositories
{
    // Read side
    public interface IProjectionRepository<TAggregate> where TAggregate : class
    {
        Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TAggregate?> GetByAsync(Expression<Func<TAggregate, bool>> expression, CancellationToken cancellationToken = default);
        Task<IEnumerable<TAggregate>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
