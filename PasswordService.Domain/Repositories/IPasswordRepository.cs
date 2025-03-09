using PasswordService.Domain.Entities;
using RepositoryLib.Interfaces.Async;

namespace PasswordService.Domain.Repositories
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
