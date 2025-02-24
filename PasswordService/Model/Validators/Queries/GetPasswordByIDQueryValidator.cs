using FluentValidation;
using PasswordService.Model.CQRS.Queries;

namespace PasswordService.Model.Validators.Queries
{
    public class GetPasswordByIDQueryValidator : AbstractValidator<GetPasswordByIDQuery>
    {
        private const int MIN_ID_VALUE = 0;
        public GetPasswordByIDQueryValidator()
        {
            RuleFor(p => p.PasswordID).GreaterThanOrEqualTo(MIN_ID_VALUE);
        }
    }
}
