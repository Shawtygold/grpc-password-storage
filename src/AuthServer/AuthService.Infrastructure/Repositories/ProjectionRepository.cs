using AuthService.Application.Abstractions.Repositories;
using AuthService.Domain.Entities;
using Marten;
using System.Linq.Expressions;

namespace AuthService.Infrastructure.Repositories
{
    public class ProjectionRepository : IProjectionRepository<UserView>
    {
        private readonly IQuerySession _querySession;

        public ProjectionRepository(IQuerySession querySession)
        {
            _querySession = querySession;
        }

        public async Task<UserView?> GetUserByAsync(Expression<Func<UserView, bool>> expression, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            return await _querySession.Query<UserView>().FirstOrDefaultAsync(expression, cancellation);
        }
    }
}
