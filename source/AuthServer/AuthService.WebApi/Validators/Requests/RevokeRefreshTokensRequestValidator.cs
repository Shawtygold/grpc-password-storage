using FluentValidation;
using GrpcAuthService;

namespace AuthService.WebApi.Validators.Requests
{
    public class RevokeRefreshTokensRequestValidator : AbstractValidator<RevokeRefreshTokensRequest>
    {
        public RevokeRefreshTokensRequestValidator()
        {
            RuleFor(r => r.UserId).NotEmpty();
        }
    }
}
