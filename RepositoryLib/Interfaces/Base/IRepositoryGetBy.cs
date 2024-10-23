using System.Linq.Expressions;

namespace RepositoryLib.Interfaces.Base
{
    public interface IRepositoryGetBy<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> expression);
    }
}
