using PasswordBoxGrpcServer.Interfaces;
using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services
{
    public sealed class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepository _userRepository;

        public UserRegistrationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Register(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            await _userRepository.AddAsync(user);
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
        }
    }
}
