using FluentValidation;
using GrpcAuthService;

namespace WebApi.Validators
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
