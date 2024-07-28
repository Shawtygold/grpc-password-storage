namespace PasswordBoxGrpcServer.Interfaces.Repositories.Base
{
    public interface IRepositoryAdd<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
    }
}
