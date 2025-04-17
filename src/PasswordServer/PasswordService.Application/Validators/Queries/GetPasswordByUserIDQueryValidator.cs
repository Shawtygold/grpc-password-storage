using FluentValidation;
using PasswordService.Application.CQRS.Queries.GetPasswordsByUserID;

namespace PasswordService.Application.Validators.Queries
{
    public class GetPasswordByUserIDQueryValidator : AbstractValidator<GetPasswordsByUserIDQuery>
    {
        public GetPasswordByUserIDQueryValidator()
        {
            RuleFor(p => p.UserId).NotEmpty();
        }
    }
}
