using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services.Passwords
{
    public interface IPasswordService
    {
        Password CreatePassword(string userLogin, string title, string login, string passwordHash, string commentary, string image);
        Task AddPasswordAsync(Password password);
        Task UpdatePasswordAsync(Password password);
        Task DeletePasswordAsync(int passwordId);
        IEnumerable<Password> GetAllPasswords();
        Task<IEnumerable<Password>> GetPasswordsByUserLoginAsync(string userLogin);
    }
}
