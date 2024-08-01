using FluentValidation;
using Grpc.Core;
using PasswordBoxGrpcServer.Interfaces;
using PasswordBoxGrpcServer.Model.Entities;
using PasswordBoxGrpcServer.Model.Exceptions;

namespace PasswordBoxGrpcServer.Services
{
    public sealed class PasswordBoxService : PasswordBox.PasswordBoxBase
    {
        private readonly ILogger<PasswordBoxService> _logger;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUserCreatorService _userCreatorService;
        private readonly IUserAuthenticationService _userAuthenticationService;

        public PasswordBoxService(ILogger<PasswordBoxService> logger,
            IUserRegistrationService registrationService, 
            IUserCreatorService userCreatorService,
            IUserAuthenticationService userAuthenticationService)
        {
            _logger = logger;
            _userRegistrationService = registrationService;
            _userCreatorService = userCreatorService;
            _userAuthenticationService = userAuthenticationService;
        }

        public override async Task<RegisterUserReply> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            try
            {
                User user = _userCreatorService.Create(request.Login, request.Password);
                await _userRegistrationService.Register(user);
            }
            catch (ValidationException ex)
            {
                string msg = "User validation exception";
                _logger.LogError(AppLogEvents.Exception, ex, msg);
                throw new RpcException(new Status(StatusCode.InvalidArgument, msg));
            }
            catch (Exception ex)
            {
                _logger.LogError(AppLogEvents.Exception, ex, "Default exception");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }

            return new RegisterUserReply { Success = true };
        }

        public override async Task<AuthenticateUserReply> AuthenticateUser(AuthenticateUserRequest request, ServerCallContext context)
        {
            bool result = false;

            try
            {
                User user = _userCreatorService.Create(request.Login, request.Password);
                result = await _userAuthenticationService.Authenticate(user);
            }
            catch (ValidationException ex)
            {
                string msg = "User validation exception";
                _logger.LogError(AppLogEvents.Exception, ex, msg);
                throw new RpcException(new Status(StatusCode.InvalidArgument, msg));
            }
            catch (Exception ex)
            {
                _logger.LogError(AppLogEvents.Exception, ex.Message, "Default exception");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }

            return new AuthenticateUserReply { Success = result };
        }
    }
}
