using System.Linq.Expressions;

namespace RepositoryLib.Interfaces.Base
{
    internal interface IRepositoryGetCollectionBy<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetCollectionBy(Expression<Func<TEntity, bool>> expression);
    }
}
