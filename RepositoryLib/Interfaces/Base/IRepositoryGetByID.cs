namespace RepositoryLib.Interfaces.Base
{
    public interface IRepositoryGetByID<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIDAsync(int entityId);
    }
}
