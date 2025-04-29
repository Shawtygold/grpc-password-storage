using AuthService.Application.CQRS.Queries.GetRefreshToken;
using FluentValidation;

namespace AuthService.Application.Validators.Queries
{
    public class GetRefreshTokenByTokenQueryValidator : AbstractValidator<GetRefreshTokenByTokenQuery>
    {
        public GetRefreshTokenByTokenQueryValidator()
        {
            RuleFor(r => r.Token).NotEmpty();
        }
    }
}
