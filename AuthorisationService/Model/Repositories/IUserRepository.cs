using AuthService.Model.Entities;
using RepositoryLib.Interfaces.Async;

namespace AuthService.Model.Repositories
{
    public interface IUserRepository :
     IRepositoryAddAsync<User>,
     IRepositoryGetByAsync<User>,
     IDisposable
    {
    }
}
