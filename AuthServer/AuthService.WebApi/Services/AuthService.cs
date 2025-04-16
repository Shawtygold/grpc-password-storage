using AuthService.Application.Abstractions.Services;
using AuthService.Application.CQRS.Commands.RegisterUser;
using AuthService.WebApi.Extensions;
using Grpc.Core;
using GrpcAuthService;
using Wolverine;

namespace AuthService.WebApi.Services
{
    public class AuthService : AuthProtoService.AuthProtoServiceBase
    {
        private readonly IMessageBus _messageBus;
        private readonly IUserAuthenticator _userAuthenticator;

        public AuthService(IMessageBus messageBus, IUserAuthenticator userAuthenticator)
        {
            _messageBus = messageBus;
            _userAuthenticator = userAuthenticator;
        }

        public override async Task<AuthenticateUserResponse> AuthenticateUser(AuthenticateUserRequest request, ServerCallContext context)
        {
            AuthenticateUserResponse response;
            CancellationToken cancellation = context.CancellationToken;

            string jwtToken = await _userAuthenticator.AuthenticateAsync(request.Login, request.Password, cancellation);
            response = new() { Token = jwtToken };           

            return response;
        }

        public override async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            RegisterUserResponse response;
            CancellationToken cancellation = context.CancellationToken;

            RegisterUserCommand command = request.ToRegisterUserCommand();
            Guid userId = await _messageBus.InvokeAsync<Guid>(command, cancellation);

            response = new() { UserId = userId.ToString() };

            return response;
        }
    }
}
