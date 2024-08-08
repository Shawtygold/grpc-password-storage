using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services.Users
{
    public interface IUserAuthenticator
    {
        Task<bool> AuthenticateAsync(User user);
    }
}
