namespace PasswordBoxGrpcServer.Interfaces.Repositories.Base
{
    public interface IRepositoryDelete
    {
        Task DeleteAsync(int entityId);
    }
}
