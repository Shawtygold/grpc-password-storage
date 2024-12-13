namespace RepositoryLib.Interfaces.Async
{
    public interface IRepositoryDeleteAsync
    {
        Task DeleteAsync(int entityId);
    }
}
