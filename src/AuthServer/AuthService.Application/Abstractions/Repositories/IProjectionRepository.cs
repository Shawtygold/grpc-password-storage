using System.Linq.Expressions;

namespace AuthService.Application.Abstractions.Repositories
{
    public interface IProjectionRepository<TAggregate> where TAggregate : class
    {
        Task<TAggregate?> GetUserByAsync(Expression<Func<TAggregate, bool>> expression, CancellationToken cancellation = default);
    }
}
