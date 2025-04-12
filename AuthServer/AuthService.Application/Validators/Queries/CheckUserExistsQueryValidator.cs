using AuthService.Application.CQRS.Queries.CheckUserExists;
using FluentValidation;

namespace AuthService.Application.Validators.Queries
{
    public class CheckUserExistsQueryValidator : AbstractValidator<CheckUserExistsQuery>
    {
        public CheckUserExistsQueryValidator()
        {
            RuleFor(q => q.Login).NotEmpty();
        }
    }
}
