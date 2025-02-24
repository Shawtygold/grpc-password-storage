namespace RepositoryLib.Interfaces.Async
{
    public interface IRepositoryExistsAsync
    {
        Task<bool> ExistsAsync(int entityId);
    }
}
