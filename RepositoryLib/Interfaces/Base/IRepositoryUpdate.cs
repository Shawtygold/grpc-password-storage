namespace RepositoryLib.Interfaces.Base
{
    public interface IRepositoryUpdate<TEntity> where TEntity : class
    {
        Task UpdateAsync(TEntity entity);
    }
}
