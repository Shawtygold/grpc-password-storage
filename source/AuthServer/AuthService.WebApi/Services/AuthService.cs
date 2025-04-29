using AuthService.Application.Abstractions.Services;
using AuthService.Application.CQRS.Commands.DeleteRefreshTokens;
using Grpc.Core;
using GrpcAuthService;
using Wolverine;

namespace AuthService.WebApi.Services
{
    public class AuthService : AuthProtoService.AuthProtoServiceBase
    {
        private readonly ILoginService _loginService;
        private readonly IRegistrationService _registrationService;
        private readonly IMessageBus _messageBus;

        public AuthService(ILoginService loginService, IRegistrationService registrationService, IMessageBus messageBus)
        {
            _loginService = loginService;
            _registrationService = registrationService;
            _messageBus = messageBus;
        }

        public override async Task<LoginUserResponse> Login(LoginUserRequest request, ServerCallContext context)
        {
            CancellationToken cancellation = context.CancellationToken;
            cancellation.ThrowIfCancellationRequested();

            var response = await _loginService.LoginAsync(request.Login, request.Password, cancellation);

            return new LoginUserResponse { AccessToken = response.AccessToken, RefreshToken = response.RefreshToken };
        }

        public override async Task<LoginWithRefreshTokenResponse> LoginWithRefreshToken(LoginWithRefreshTokenRequest request, ServerCallContext context)
        {
            CancellationToken cancellation = context.CancellationToken;
            cancellation.ThrowIfCancellationRequested();

            var response = await _loginService.LoginWithRefreshTokenAsync(request.RefreshToken);

            return new LoginWithRefreshTokenResponse { AccessToken = response.AccessToken, RefreshToken = response.RefreshToken };
        }

        public override async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            CancellationToken cancellation = context.CancellationToken;
            cancellation.ThrowIfCancellationRequested();

            Guid userId = await _registrationService.RegisterAsync(request.Login, request.Email, request.Password, cancellation);

            return new RegisterUserResponse() { UserId = userId.ToString() };
        }

        public override async Task<RevokeRefreshTokensResponse> RevokeRefreshTokens(RevokeRefreshTokensRequest request, ServerCallContext context)
        {
            CancellationToken cancellation = context.CancellationToken;
            cancellation.ThrowIfCancellationRequested();

            DeleteRefreshTokensCommand command = new(Guid.Parse(request.UserId));
            await _messageBus.InvokeAsync(command);

            return new RevokeRefreshTokensResponse() { Success = true };
        }
    }
}
