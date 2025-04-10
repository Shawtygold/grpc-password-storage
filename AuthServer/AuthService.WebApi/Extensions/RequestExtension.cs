using AuthService.Application.CQRS.Commands.RegisterUser;
using GrpcAuthService;

namespace AuthService.WebApi.Extensions
{
    public static class RequestExtension
    {
        public static RegisterUserCommand ToRegisterUserCommand(this RegisterUserRequest request)
        {
            return new RegisterUserCommand(request.Login, request.Email, request.Password);
        }
    }
}
