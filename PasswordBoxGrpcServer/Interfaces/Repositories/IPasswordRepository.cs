using PasswordBoxGrpcServer.Model.Entities;
using RepositoryLib.Interfaces.Base;

namespace PasswordBoxGrpcServer.Interfaces.Repositories
{
    public interface IPasswordRepository :
        IRepositoryAdd<Password>,
        IRepositoryUpdate<Password>,
        IRepositoryDelete,
        IRepositoryCollectionGetBy<Password>,
        IDisposable
    {
    }
}
