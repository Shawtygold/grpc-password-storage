namespace RepositoryLib.Interfaces.Async
{
    public interface IRepositoryGetByIDAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIDAsync(int entityId);
    }
}
