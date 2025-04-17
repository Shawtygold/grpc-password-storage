using AuthService.Application.CQRS.Queries.GetUserByLogin;
using FluentValidation;

namespace AuthService.Application.Validators.Queries
{
    public class GetUserByLoginQueryValidator : AbstractValidator<GetUserByLoginQuery>
    {
        public GetUserByLoginQueryValidator()
        {
            RuleFor(q => q.Login).NotEmpty();
        }
    }
}
