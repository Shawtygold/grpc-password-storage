using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services.Users
{
    public interface IUserRegistration : IDisposable
    {
        Task RegisterAsync(User user);
    }
}
