namespace RepositoryLib.Interfaces.Async
{
    internal interface IRepositoryAddAsync<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
    }
}
