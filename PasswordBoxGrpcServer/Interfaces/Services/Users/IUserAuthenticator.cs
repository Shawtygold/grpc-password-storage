using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services.Users
{
    public interface IUserAuthenticator : IDisposable
    {
        Task<bool> AuthenticateAsync(User user);
    }
}
