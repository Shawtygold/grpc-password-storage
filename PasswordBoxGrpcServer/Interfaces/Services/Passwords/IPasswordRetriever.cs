using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Interfaces.Services.Passwords
{
    public interface IPasswordRetriever
    {
        IEnumerable<Password> GetPasswords();
    }
}
