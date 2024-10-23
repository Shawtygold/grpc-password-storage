using AuthorisationService.Model.Entities;
using RepositoryLib.Interfaces.Base;

namespace AuthorisationService.Interfaces.Repositories
{
    public interface IUserRepository :
     IRepositoryAdd<User>,
     IRepositoryGetBy<User>,
     IDisposable
    {
    }
}
