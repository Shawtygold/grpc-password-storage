using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Interfaces.Services.Passwords;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services.Passwords
{
    public class PasswordRetriever : IPasswordRetriever
    {
        private readonly IPasswordRepository _passwordRepository;

        public PasswordRetriever(IPasswordRepository passwordRepository)
        {
            _passwordRepository = passwordRepository;
        }

        public IEnumerable<Password> GetPasswords()
        {
            // TODO: Add an decryptor

            return _passwordRepository.GetAll();
        }
    }
}
