using AuthService.Application.Abstractions.Services;
using Grpc.Core;
using GrpcAuthService;

namespace AuthService.WebApi.Services
{
    public class AuthService : AuthProtoService.AuthProtoServiceBase
    {
        private readonly ILoginService _loginService;
        private readonly IRegistrationService _registrationService;

        public AuthService(ILoginService loginService, IRegistrationService registrationService)
        {
            _loginService = loginService;
            _registrationService = registrationService;
        }

        public override async Task<LoginUserResponse> Login(LoginUserRequest request, ServerCallContext context)
        {
            CancellationToken cancellation = context.CancellationToken;
            cancellation.ThrowIfCancellationRequested();

            string jwtToken = await _loginService.LoginAsync(request.Login, request.Password, cancellation);

            LoginUserResponse response = new() { Token = jwtToken };           
            return response;
        }

        public override async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            CancellationToken cancellation = context.CancellationToken;
            cancellation.ThrowIfCancellationRequested();

            Guid userId = await _registrationService.RegisterAsync(request.Login, request.Email, request.Password, cancellation);

            RegisterUserResponse response = new() { UserId = userId.ToString() };
            return response;
        }
    }
}
