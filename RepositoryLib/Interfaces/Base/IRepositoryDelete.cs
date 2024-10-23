namespace RepositoryLib.Interfaces.Base
{
    public interface IRepositoryDelete
    {
        Task DeleteAsync(int entityId);
    }
}
