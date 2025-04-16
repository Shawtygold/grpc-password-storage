using AuthService.Application.CQRS.Queries.CheckUserExists;
using FluentValidation;
using GrpcAuthService;
using System.Net.Mail;
using Wolverine;

namespace AuthService.WebApi.Validators.Requests
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        private readonly IMessageBus _messageBus;

        public RegisterUserRequestValidator(IMessageBus messageBus)
        {
            _messageBus = messageBus;

            RuleFor(c => c.Login).NotEmpty().MustAsync(async (login, cancellation) => await IsLoginUnique(login, cancellation)).WithMessage("This login already exists");
            RuleFor(c => c.Email).NotEmpty().Must(IsValidEmail).WithMessage("Invalid email");
            RuleFor(c => c.Password).NotEmpty();
        }

        private bool IsValidEmail(string email) => MailAddress.TryCreate(email, out _);
        private async Task<bool> IsLoginUnique(string login, CancellationToken cancellationToken = default)
        {
            CheckUserExistsQuery query = new(login);
            return !await _messageBus.InvokeAsync<bool>(query, cancellationToken);
        }
    }
}
