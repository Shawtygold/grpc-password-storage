namespace RepositoryLib.Interfaces.Async
{
    internal interface IRepositoryDeleteAsync
    {
        Task DeleteAsync(int entityId);
    }
}
