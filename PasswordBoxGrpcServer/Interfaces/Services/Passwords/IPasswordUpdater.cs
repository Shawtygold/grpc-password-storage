using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services.Passwords
{
    public interface IPasswordUpdater
    {
        Task UpdatePasswordAsync(Password password);
    }
}
