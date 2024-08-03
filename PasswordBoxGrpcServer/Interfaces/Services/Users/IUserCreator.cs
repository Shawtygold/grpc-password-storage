using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services.Users
{
    public interface IUserCreator
    {
        User Create(string login, string password);
    }
}
