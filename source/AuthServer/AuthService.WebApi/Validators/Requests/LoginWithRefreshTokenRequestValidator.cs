using FluentValidation;
using GrpcAuthService;

namespace AuthService.WebApi.Validators.Requests
{
    public class LoginWithRefreshTokenRequestValidator : AbstractValidator<LoginWithRefreshTokenRequest>
    {
        public LoginWithRefreshTokenRequestValidator()
        {
            RuleFor(r => r.RefreshToken).NotEmpty();
        }
    }
}
