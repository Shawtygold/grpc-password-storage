namespace PasswordBoxGrpcServer.Interfaces.Repositories.Base
{
    public interface IRepositoryDelete<TEntity> where TEntity : class
    {
        Task DeleteAsync(TEntity entity);
    }
}
