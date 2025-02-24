using FluentValidation;
using PasswordService.Model.CQRS.Commands;

namespace PasswordService.Model.Validators.Commands
{
    public class DeletePasswordCommandValidator : AbstractValidator<DeletePasswordCommand>
    {
        private const int MIN_ID_VALUE = 0;
        public DeletePasswordCommandValidator()
        {
            RuleFor(c => c.PasswordId).GreaterThanOrEqualTo(MIN_ID_VALUE);
        }
    }
}
