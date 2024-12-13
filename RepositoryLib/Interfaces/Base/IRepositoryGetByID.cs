namespace RepositoryLib.Interfaces.Base
{
    public interface IRepositoryGetByID<TEntity> where TEntity : class
    {
        TEntity? GetByID(int entityId);
    }
}
