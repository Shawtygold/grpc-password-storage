using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services.Users
{
    public interface IUserRegistration
    {
        Task<bool> RegisterAsync(User user);
    }
}
