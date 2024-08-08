using PasswordBoxGrpcServer.Model.Entities;
using PasswordBoxGrpcServer.Services.Users;

namespace PasswordBoxGrpcServer.Interfaces.Services.Users
{
    public interface IUserService
    {
        Task<bool> AuthenticateAsync(User user);
        User CreateUser(string login, string password);
        Task RegisterAsync(User user);
    }
}
