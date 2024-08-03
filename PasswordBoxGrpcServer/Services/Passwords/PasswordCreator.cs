using PasswordBoxGrpcServer.Interfaces.Services.Passwords;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services.Passwords
{
    public class PasswordCreator : IPasswordCreator
    {
        public Password Create(string userLogin, string title, string login, string passwordHash, string commentary, string image)
        {
            // Создание и валидация пароля
            Password password = new(userLogin, title, login, passwordHash, commentary, image);
            return password;
        }
    }
}
