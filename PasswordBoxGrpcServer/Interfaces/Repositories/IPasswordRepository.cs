using PasswordBoxGrpcServer.Interfaces.Repositories.Base;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Repositories
{
    public interface IPasswordRepository : 
        IRepositoryGetAll<Password>,
        IRepositoryAdd<Password>,
        IRepositoryUpdate<Password>,
        IRepositoryDelete, IDisposable
    {
    }
}
