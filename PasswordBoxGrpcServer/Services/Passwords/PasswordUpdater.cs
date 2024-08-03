using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Interfaces.Services.Passwords;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services.Passwords
{
    public class PasswordUpdater : IPasswordUpdater
    {
        private readonly IPasswordRepository _passwordRepository;

        public PasswordUpdater(IPasswordRepository passwordRepository)
        {
            _passwordRepository = passwordRepository;
        }

        public async Task UpdatePasswordAsync(Password password)
        {
            // TODO: Add an encryptor

            await _passwordRepository.UpdateAsync(password);
        }
    }
}
