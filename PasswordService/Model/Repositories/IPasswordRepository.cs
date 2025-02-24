using PasswordService.Model.Entities;
using RepositoryLib.Interfaces.Async;

namespace PasswordService.Model.Repositories
{
    public interface IPasswordRepository :
        IRepositoryAddAsync<Password>,
        IRepositoryUpdateAsync<Password>,
        IRepositoryDeleteAsync,
        IRepositoryGetByAsync<Password>,
        IRepositoryGetByIDAsync<Password>,
        IRepositoryGetCollectionByAsync<Password>,
        IDisposable
    {
    }
}
