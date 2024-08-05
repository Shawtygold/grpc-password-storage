using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services.Passwords
{
    public interface IPasswordCreator
    {
        Password Create(string userLogin, string title, string login, string passwordHash, string commentary, string image);
    }
}
