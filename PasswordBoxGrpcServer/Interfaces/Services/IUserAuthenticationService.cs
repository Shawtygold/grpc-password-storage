using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces
{
    public interface IUserAuthenticationService : IDisposable
    {
        Task<bool> Authenticate(User user);
    }
}
