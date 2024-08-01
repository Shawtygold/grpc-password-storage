using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces
{
    public interface IUserRegistrationService : IDisposable
    {
        Task Register(User user);
    }
}
