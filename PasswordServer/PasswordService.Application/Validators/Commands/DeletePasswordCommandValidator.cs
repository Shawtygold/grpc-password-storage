using FluentValidation;
using PasswordService.Application.CQRS.Commands.DeletePassword;

namespace PasswordService.Application.Validators.Commands
{
    public class DeletePasswordCommandValidator : AbstractValidator<DeletePasswordCommand>
    {
        public DeletePasswordCommandValidator()
        {
            RuleFor(c => c.PasswordId).NotEmpty();
        }
    }
}
