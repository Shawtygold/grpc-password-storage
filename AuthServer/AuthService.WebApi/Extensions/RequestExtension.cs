using AuthService.Application.DTO;
using GrpcAuthService;

namespace WebApi.Extensions
{
    public static class RequestExtension
    {
        public static RegisterUserDTO ToDTO(this RegisterUserRequest request)
        {
            return new RegisterUserDTO(request.Login, request.Email, request.Password);
        }
    }
}
