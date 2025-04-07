using AuthService.Application.Abstractions.Services;
using AuthService.Application.Exceptions;
using FluentValidation;
using Grpc.Core;
using GrpcAuthService;
using WebApi.Exceptions;
using WebApi.Extensions;

namespace WebApi.Services
{
    public class AuthService : AuthProtoService.AuthProtoServiceBase
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IUserAuthenticator _userAuthenticator;
        private readonly IUserRegistration _userRegistration;
        private readonly IValidator<AuthenticateUserRequest> _authUserRequestValidator;
        private static readonly string _domain = AppDomain.CurrentDomain.FriendlyName;

        public AuthService(ILogger<AuthService> logger,
            IUserAuthenticator userAuthenticator,
            IUserRegistration userRegistration,
            IValidator<AuthenticateUserRequest> authUserRequestValidator)
        {
            _logger = logger;
            _userAuthenticator = userAuthenticator;
            _userRegistration = userRegistration;
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
                Guid userId = await _userRegistration.RegisterAsync(request.ToDTO());
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
