using AuthService.Application.Abstractions.Services;
using AuthService.Application.CQRS.Commands.RegisterUser;
using AuthService.Application.CQRS.Queries.CheckUserExists;
using AuthService.WebApi.Abstractions;
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
        private readonly IGrpcExceptionMapper _grpcExceptionMapper;

        public AuthService(ILogger<AuthService> logger,
            IMessageBus messageBus,
            IUserAuthenticator userAuthenticator,
            IValidator<AuthenticateUserRequest> authUserRequestValidator,
            IGrpcExceptionMapper grpcExceptionMapper)
        {
            _logger = logger;
            _messageBus = messageBus;
            _userAuthenticator = userAuthenticator;
            _authUserRequestValidator = authUserRequestValidator;
            _grpcExceptionMapper = grpcExceptionMapper;
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
            catch (Exception ex)
            {              
                throw _grpcExceptionMapper.MapException(ex);
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
            catch (Exception ex)
            {
                throw _grpcExceptionMapper.MapException(ex);
            }

            _logger.LogInformation("{Date} {Operation} User {UserLogin} has been registered", DateTime.Now, nameof(RegisterUser), request.Login);

            return response;
        }

        public override async Task<CheckUserExistsResponse> CheckUserExists(CheckUserExistsRequest request, ServerCallContext context)
        {
            CheckUserExistsResponse response;

            try
            {
                CheckUserExistsQuery query = new(request.Login);
                response = new() { Exists = await _messageBus.InvokeAsync<bool>(query) };
            }
            catch (Exception ex)
            {
                throw _grpcExceptionMapper.MapException(ex);
            }

            return response;
        }
    }
}
