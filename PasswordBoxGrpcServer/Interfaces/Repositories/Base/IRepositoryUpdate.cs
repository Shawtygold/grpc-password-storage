namespace PasswordBoxGrpcServer.Interfaces.Repositories.Base
{
    public interface IRepositoryUpdate<TEntity> where TEntity : class
    {
        Task UpdateAsync(TEntity entity);
    }
}
