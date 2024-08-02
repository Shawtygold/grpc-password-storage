namespace PasswordBoxGrpcServer.Interfaces.Repositories.Base
{
    public interface IRepositoryGetAll<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
    }
}
