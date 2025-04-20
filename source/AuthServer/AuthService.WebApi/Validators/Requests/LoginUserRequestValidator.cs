using FluentValidation;
using GrpcAuthService;

namespace AuthService.WebApi.Validators.Requests
{
    public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserRequestValidator()
        {
            RuleFor(r => r.Login).NotEmpty();
            RuleFor(r => r.Password).NotEmpty();
        }
    }
}
