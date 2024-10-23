using PasswordService.Model.Entities;
using RepositoryLib.Interfaces.Base;

namespace PasswordService.Interfaces.Repositories
{
    public interface IPasswordRepository :
        IRepositoryAdd<Password>,
        IRepositoryUpdate<Password>,
        IRepositoryGetBy<Password>,
        IRepositoryDelete,
        IRepositoryCollectionGetBy<Password>,
        IDisposable
    {
    }
}
