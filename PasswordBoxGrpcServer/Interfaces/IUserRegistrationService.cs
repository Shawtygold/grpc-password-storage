using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces
{
    public interface IUserRegistrationService
    {
        Task Register(User user);
    }
}
