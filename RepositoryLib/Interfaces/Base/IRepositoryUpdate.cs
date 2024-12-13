namespace RepositoryLib.Interfaces.Base
{
    public interface IRepositoryUpdate<TEntity> where TEntity : class
    {
        void Update(TEntity entity);
    }
}
