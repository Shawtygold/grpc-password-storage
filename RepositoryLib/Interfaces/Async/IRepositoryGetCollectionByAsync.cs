using System.Linq.Expressions;

namespace RepositoryLib.Interfaces.Async
{
    public interface IRepositoryGetCollectionByAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetCollectionByAsync(Expression<Func<TEntity, bool>> expression);
    }
}
