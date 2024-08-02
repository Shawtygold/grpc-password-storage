using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services
{
    public interface IUserRegistrationService : IDisposable
    {
        Task Register(User user);
    }
}
