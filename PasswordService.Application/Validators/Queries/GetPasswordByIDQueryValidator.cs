using FluentValidation;
using PasswordService.Application.CQRS.Queries.GetPasswordByID;

namespace PasswordService.Application.Validators.Queries
{
    public class GetPasswordByIDQueryValidator : AbstractValidator<GetPasswordByIDQuery>
    {
        public GetPasswordByIDQueryValidator()
        {
            RuleFor(p => p.PasswordID).NotEmpty();
        }
    }
}
