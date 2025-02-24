using FluentValidation;
using PasswordService.Model.CQRS.Queries;

namespace PasswordService.Model.Validators.Queries
{
    public class GetPasswordByUserIDQueryValidator : AbstractValidator<GetPasswordsByUserIDQuery>
    {
        private const int MIN_ID_VALUE = 0;
        public GetPasswordByUserIDQueryValidator()
        {
            RuleFor(p => p.UserID).GreaterThanOrEqualTo(MIN_ID_VALUE);
        }
    }
}
