using FluentValidation;
using PasswordService.Model.CQRS.Commands;

namespace PasswordService.Model.Validators.Commands
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        private const int MIN_ID_VALUE = 0;
        public UpdatePasswordCommandValidator()
        {
            RuleFor(c => c.ID).NotEmpty().GreaterThanOrEqualTo(MIN_ID_VALUE);
            RuleFor(c => c.UserID).NotEmpty().GreaterThanOrEqualTo(MIN_ID_VALUE);
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.Login).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.IconPath).NotEmpty();
        }
    }
}
