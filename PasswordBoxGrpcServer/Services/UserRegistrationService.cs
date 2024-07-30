using PasswordBoxGrpcServer.Interfaces;
using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Model.Entities;
using PasswordBoxGrpcServer.Model.Repositories;

namespace PasswordBoxGrpcServer.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepository _userRepository;

        public UserRegistrationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Register(User user)
        {
            await _userRepository.AddAsync(user);
        }
    }
}
