using PasswordBoxGrpcServer.Interfaces.Repositories.Base;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Repositories
{
    public interface IUserRepository : 
        IRepositoryAdd<User>,
        IRepositoryGetBy<User>,
        IDisposable
    {
    }
}
