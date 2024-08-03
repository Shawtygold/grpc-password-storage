using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services.Passwords
{
    public interface IPasswordCreator
    {
        Password CreatePassword(string userLogin, string title, string login, string passwordHash, string commentary, string image);
    }
}
