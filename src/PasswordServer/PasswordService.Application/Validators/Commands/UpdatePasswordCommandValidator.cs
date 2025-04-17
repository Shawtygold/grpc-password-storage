using FluentValidation;
using PasswordService.Application.CQRS.Commands.UpdatePassword;

namespace PasswordService.Application.Validators.Commands
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.Login).NotEmpty();
            RuleFor(c => c.EncryptedPassword).NotEmpty();
            RuleFor(c => c.IconPath).NotEmpty();
        }
    }
}
