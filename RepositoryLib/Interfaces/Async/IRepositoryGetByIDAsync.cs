namespace RepositoryLib.Interfaces.Async
{
    internal interface IRepositoryGetByIDAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIDAsync(int entityId);
    }
}
