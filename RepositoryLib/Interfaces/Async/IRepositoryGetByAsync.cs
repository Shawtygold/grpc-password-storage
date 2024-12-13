using System.Linq.Expressions;

namespace RepositoryLib.Interfaces.Async
{
    public interface IRepositoryGetByAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> expression);
    }
}
