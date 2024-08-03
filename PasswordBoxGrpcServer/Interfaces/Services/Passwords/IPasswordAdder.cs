using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services.Passwords
{
    public interface IPasswordAdder
    {
        Task AddPasswordAsync(Password password);
    }
}
