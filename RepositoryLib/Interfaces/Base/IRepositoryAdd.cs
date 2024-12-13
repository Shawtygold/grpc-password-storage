namespace RepositoryLib.Interfaces.Base
{
    public interface IRepositoryAdd<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
    }
}
