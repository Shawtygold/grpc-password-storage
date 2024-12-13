using System.Linq.Expressions;

namespace RepositoryLib.Interfaces.Base
{
    public interface IRepositoryGetBy<TEntity> where TEntity : class
    {
        TEntity? GetBy(Expression<Func<TEntity, bool>> expression);
    }
}
