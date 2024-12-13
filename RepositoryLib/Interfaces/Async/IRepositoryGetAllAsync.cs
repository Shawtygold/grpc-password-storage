namespace RepositoryLib.Interfaces.Async
{
    public interface IRepositoryGetAllAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
