using AuthService.Application.CQRS.Commands.RegisterUser;
using FluentValidation;

namespace AuthService.Application.Validators.Commands
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(c => c.Login).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.Email).NotEmpty();
        }
    }
}
