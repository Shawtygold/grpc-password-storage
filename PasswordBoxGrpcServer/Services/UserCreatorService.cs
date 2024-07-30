using PasswordBoxGrpcServer.Interfaces;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services
{
    public class UserCreatorService : IUserCreatorService
    {
        public User Create(string login, string password)
        {
            // Валидация и создание пользователя
            User user = new(login, password);
            return user;
        }
    }
}
