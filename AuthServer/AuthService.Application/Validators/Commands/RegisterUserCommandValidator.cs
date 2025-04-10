using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.CQRS.Commands.RegisterUser;
using AuthService.Domain.Entities;
using FluentValidation;
using System.Net.Mail;

namespace AuthService.Application.Validators.Commands
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private readonly IProjectionRepository<UserView> _readRepository;

        public RegisterUserCommandValidator(IProjectionRepository<UserView> readRepository)
        {
            _readRepository = readRepository;

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
            return await _readRepository.GetUserByAsync(u => u.Login == login, cancellationToken) == null;
        }
    }
}
