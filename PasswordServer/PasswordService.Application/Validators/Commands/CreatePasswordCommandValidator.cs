using FluentValidation;
using PasswordService.Application.CQRS.Commands.CreatePassword;

namespace PasswordService.Application.Validators.Commands
{
    public class CreatePasswordCommandValidator : AbstractValidator<CreatePasswordCommand>
    {
        public CreatePasswordCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.Login).NotEmpty();
            RuleFor(c => c.EncryptedPassword).NotEmpty();
            RuleFor(c => c.IconPath).NotEmpty();
        }
    }
}
