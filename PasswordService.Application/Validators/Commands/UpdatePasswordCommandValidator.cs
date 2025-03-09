using FluentValidation;
using PasswordService.Application.CQRS.Commands.UpdatePassword;

namespace PasswordService.Application.Validators.Commands
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator()
        {
            RuleFor(c => c.ID).NotEmpty();
            RuleFor(c => c.UserID).NotEmpty();
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.Login).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.IconPath).NotEmpty();
        }
    }
}
