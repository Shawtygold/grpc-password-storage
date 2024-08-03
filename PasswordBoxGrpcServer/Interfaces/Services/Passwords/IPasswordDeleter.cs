using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services.Passwords
{
    public interface IPasswordDeleter
    {
        Task DeletePasswordAsync(int passwordId);
    }
}
