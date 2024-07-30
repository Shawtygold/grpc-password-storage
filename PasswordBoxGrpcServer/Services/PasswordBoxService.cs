using FluentValidation;
using Grpc.Core;
using PasswordBoxGrpcServer.Interfaces;
using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Model.AppContext;
using PasswordBoxGrpcServer.Model.Entities;
using PasswordBoxGrpcServer.Model.Repositories;

namespace PasswordBoxGrpcServer.Services
{
    public sealed class PasswordBoxService : PasswordBox.PasswordBoxBase
    {
        private readonly ILogger<PasswordBoxService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUserCreatorService _userCreatorService;

        public PasswordBoxService(ILogger<PasswordBoxService> logger)
        {
            _logger = logger;
            _userRepository = new UserRepository(new ApplicationContext());
            _userRegistrationService = new UserRegistrationService(_userRepository);
            _userCreatorService = new UserCreatorService();
        }

        public override async Task<RegisterUserReply> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            try
            {
                User user = _userCreatorService.Create(request.Login, request.Password);
                await _userRegistrationService.Register(user);

                return new RegisterUserReply {Success = true};
            }
            catch (ValidationException ex)
            {
                _logger.LogError(0, ex, "Validation exception");
            }
            catch (Exception ex)
            {
                _logger.LogError(1, ex, "Default exception");
            }

            return new RegisterUserReply() { Success = false };
        }
    }
}
