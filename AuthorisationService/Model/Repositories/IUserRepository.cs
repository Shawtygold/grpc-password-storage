using AuthorisationService.Model.Entities;
using RepositoryLib.Interfaces.Async;

namespace AuthorisationService.Model.Repositories
{
    public interface IUserRepository :
     IRepositoryAddAsync<User>,
     IRepositoryGetByAsync<User>,
     IDisposable
    {
    }
}
