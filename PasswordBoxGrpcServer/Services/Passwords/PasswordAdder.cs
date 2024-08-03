using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Interfaces.Services.Passwords;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services.Passwords
{
    public class PasswordAdder : IPasswordAdder
    {
        private readonly IPasswordRepository _passwordRepository;

        public PasswordAdder(IPasswordRepository passwordRepository)
        {
            _passwordRepository = passwordRepository;
        }

        public async Task AddPassword(Password password)
        {
            // TODO: Add an encryptor

            await _passwordRepository.AddAsync(password);
        }
    }
}
