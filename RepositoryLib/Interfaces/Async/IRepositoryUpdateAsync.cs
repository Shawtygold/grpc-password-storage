namespace RepositoryLib.Interfaces.Async
{
    public interface IRepositoryUpdateAsync<TEntity> where TEntity : class
    {
        Task UpdateAsync(TEntity entity);
    }
}
