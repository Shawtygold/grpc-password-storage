using PasswordBoxGrpcServer.Interfaces.Repositories.Base;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Repositories
{
    public interface IPasswordRepository :
        IRepositoryAdd<Password>,
        IRepositoryUpdate<Password>,
        IRepositoryDelete,
        IRepositoryGetAll<Password>,
        IRepositoryCollectionGetBy<Password>,
        IRepositoryGetBy<Password>,
        IDisposable
    {
    }
}
