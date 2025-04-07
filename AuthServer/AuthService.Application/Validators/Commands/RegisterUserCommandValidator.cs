using AuthService.Application.CQRS.Commands.RegisterUser;
using AuthService.Domain.Entities;
using FluentValidation;
using Marten;
using System.Net.Mail;

namespace AuthService.Application.Validators.Commands
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private readonly IQuerySession _queerySession;

        public RegisterUserCommandValidator(IQuerySession querySession)
        {
            _queerySession = querySession;

            RuleFor(c => c.Login).NotEmpty().MustAsync(async (login, cancellation) => await IsValidLogin(login, cancellation)).WithMessage("This login already exists");
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

        private async Task<bool> IsValidLogin(string login, CancellationToken cancellationToken)
        {
            return await _queerySession.Query<User>().FirstOrDefaultAsync(u => u.Login == login, cancellationToken) == null;
        }
    }
}
