using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services.Passwords
{
    public interface IPasswordService
    {
        Task<Password> AddPasswordAsync(Password password);
        Task<Password> UpdatePasswordAsync(Password password);
        Task DeletePasswordAsync(int passwordId);
        Task<IEnumerable<Password>> GetPasswordsByUserLoginAsync(string userLogin);
    }
}
