using PasswordBoxGrpcServer.Interfaces.Services.Users;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services.Users
{
    public class UserCreator : IUserCreator
    {
        public User Create(string login, string password)
        {
            // Validation and object creation 
            User user = new(login, password);
            return user;
        }
    }
}
