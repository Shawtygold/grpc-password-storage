namespace RepositoryLib.Interfaces.Async
{
    internal interface IRepositoryGetAllAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
