using System.Linq.Expressions;

namespace PasswordBoxGrpcServer.Interfaces.Repositories.Base
{
    public interface IRepositoryGetBy<TEntity> where TEntity : class
    {
        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expression);
    }
}
