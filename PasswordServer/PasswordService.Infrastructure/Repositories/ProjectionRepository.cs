using Marten;
using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Domain.Entities;
using System.Linq.Expressions;

namespace PasswordService.Infrastructure.Repositories
{
    // Read side
    public class ProjectionRepository : IProjectionRepository<PasswordView>
    {
        private readonly IQuerySession _querySession;

        public ProjectionRepository(IQuerySession querySession)
        {
            _querySession = querySession;
        }

        public async Task<IEnumerable<PasswordView>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _querySession.Query<PasswordView>().Where(p => p.UserId == userId).ToListAsync(cancellationToken);
        }

        public async Task<PasswordView?> GetByAsync(Expression<Func<PasswordView, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _querySession.Query<PasswordView>().FirstOrDefaultAsync(expression, cancellationToken);
        }

        public async Task<PasswordView?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _querySession.LoadAsync<PasswordView>(id, cancellationToken);
        }
    }
}
