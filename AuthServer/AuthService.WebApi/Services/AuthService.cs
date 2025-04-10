using AuthService.Application.Abstractions.Services;
using AuthService.Application.CQRS.Commands.RegisterUser;
using AuthService.Application.Exceptions;
using AuthService.WebApi.Exceptions;
using AuthService.WebApi.Extensions;
using FluentValidation;
using Grpc.Core;
using GrpcAuthService;
using Wolverine;

namespace AuthService.WebApi.Services
{
    public class AuthService : AuthProtoService.AuthProtoServiceBase
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IMessageBus _messageBus;
        private readonly IUserAuthenticator _userAuthenticator;
        private readonly IValidator<AuthenticateUserRequest> _authUserRequestValidator;
        private static readonly string _domain = AppDomain.CurrentDomain.FriendlyName;

        public AuthService(ILogger<AuthService> logger,
            IMessageBus messageBus,
            IUserAuthenticator userAuthenticator,
            IValidator<AuthenticateUserRequest> authUserRequestValidator)
        {
            _logger = logger;
            _messageBus = messageBus;
            _userAuthenticator = userAuthenticator;
            _authUserRequestValidator = authUserRequestValidator;
        }

        public override async Task<AuthenticateUserResponse> AuthenticateUser(AuthenticateUserRequest request, ServerCallContext context)
        {
            AuthenticateUserResponse response;

            try
            {
                await _authUserRequestValidator.ValidateAndThrowAsync(request);
                string jwtToken = await _userAuthenticator.AuthenticateAsync(request.Login, request.Password);

                response = new() { Token = jwtToken };
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError("{Date} {Operation} Failure \"{Message}\"", DateTime.Now, nameof(AuthenticateUser), ex.Message);
                throw AuthRpcExceptions.NotFound(_domain, request.Login);
            }
            catch (ValidationException ex)
            {
                _logger.LogError("{Date} {Operation} Failure \"{Message}\"", DateTime.Now, nameof(AuthenticateUser), ex.Message);
                throw AuthRpcExceptions.InvalidArgumets(ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Date} {Operation} Failure \"{Message}\"", DateTime.Now, nameof(AuthenticateUser), ex.Message);
                throw AuthRpcExceptions.InternalError(_domain, ex);
            }

            _logger.LogInformation("{Date} {Operation} User {UserLogin} has been authenticated", DateTime.Now, nameof(AuthenticateUser), request.Login);

            return response;
        }

        public override async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            RegisterUserResponse response;

            try
            {
                RegisterUserCommand command = request.ToRegisterUserCommand();
                Guid userId = await _messageBus.InvokeAsync<Guid>(command);

                response = new() { UserId = userId.ToString() };
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning("{Date} {Operation} Validation failed \"{Message}\"", DateTime.Now, nameof(RegisterUser), ex.Message);
                throw AuthRpcExceptions.InvalidArgumets(ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Date} {Operation} Failed \"{Message}\"", DateTime.Now, nameof(RegisterUser), ex.Message);
                throw AuthRpcExceptions.InternalError(_domain, ex);
            }

            _logger.LogInformation("{Date} {Operation} User {UserLogin} has been registered", DateTime.Now, nameof(RegisterUser), request.Login);

            return response;
        }
    }
}
