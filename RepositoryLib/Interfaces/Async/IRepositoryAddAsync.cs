namespace RepositoryLib.Interfaces.Async
{
    public interface IRepositoryAddAsync<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
    }
}
