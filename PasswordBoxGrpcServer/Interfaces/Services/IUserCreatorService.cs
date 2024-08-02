using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services
{
    public interface IUserCreatorService
    {
        User Create(string login, string password);
    }
}
