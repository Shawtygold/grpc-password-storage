using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Interfaces.Services.Users;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services.Users
{
    public class UserRegistration : IUserRegistration
    {
        private readonly IUserRepository _userRepository;

        public UserRegistration(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(User user)
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
