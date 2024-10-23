using System.Linq.Expressions;

namespace RepositoryLib.Interfaces.Base
{
    public interface IRepositoryCollectionGetBy <TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetCollectionBy(Expression<Func<TEntity, bool>> expression);
    }
}
