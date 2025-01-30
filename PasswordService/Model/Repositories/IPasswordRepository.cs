using PasswordService.Model.Entities;
using RepositoryLib.Interfaces.Async;

namespace PasswordService.Interfaces.Repositories
{
    public interface IPasswordRepository :
        IRepositoryAddAsync<Password>,
        IRepositoryUpdateAsync<Password>,
        IRepositoryGetByAsync<Password>,
        IRepositoryGetByIDAsync<Password>,
        IRepositoryDeleteAsync,
        IRepositoryGetCollectionByAsync<Password>,
        IDisposable
    {
    }
}
