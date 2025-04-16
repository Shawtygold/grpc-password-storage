using FluentValidation;
using GrpcAuthService;

namespace AuthService.WebApi.Validators.Requests
{
    public class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
    {
        public AuthenticateUserRequestValidator()
        {
            RuleFor(r => r.Login).NotEmpty();
            RuleFor(r => r.Password).NotEmpty();
        }
    }
}
