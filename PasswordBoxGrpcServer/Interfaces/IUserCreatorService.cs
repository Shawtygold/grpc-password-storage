using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces
{
    public interface IUserCreatorService
    {
        User Create(string login, string password);
    }
}
