using PasswordBoxGrpcServer.Model.Entities;
using PasswordBoxGrpcServer.Services.Users;

namespace PasswordBoxGrpcServer.Interfaces.Services.Users
{
    public interface IUserService
    {
        Task<bool> AuthenticateAsync(User user);
        Task RegisterAsync(User user);
    }
}
