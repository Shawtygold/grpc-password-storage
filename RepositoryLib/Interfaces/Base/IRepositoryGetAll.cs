namespace RepositoryLib.Interfaces.Base
{
    public interface IRepositoryGetAll<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
    }
}
