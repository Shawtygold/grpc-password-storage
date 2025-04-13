using AuthService.Application.CQRS.Commands.RegisterUser;
using AuthService.Application.CQRS.Queries.CheckUserExists;
using FluentValidation;
using System.Net.Mail;
using Wolverine;

namespace AuthService.Application.Validators.Commands
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private readonly IMessageBus _messageBus;

        public RegisterUserCommandValidator(IMessageBus messageBus)
        {
            _messageBus = messageBus;

            RuleFor(c => c.Login).NotEmpty().MustAsync(async (login, cancellation) => await IsLoginUnique(login, cancellation)).WithMessage("This login already exists");
            RuleFor(c => c.Email).NotEmpty().Must(IsValidEmail).WithMessage("Invalid email");
            RuleFor(c => c.Password).NotEmpty();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private async Task<bool> IsLoginUnique(string login, CancellationToken cancellationToken)
        {
            CheckUserExistsQuery query = new(login);
            return !await _messageBus.InvokeAsync<bool>(query);
        }
    }
}
