using AuthService.Application.CQRS.Commands.CreateRefreshToken;
using FluentValidation;

namespace AuthService.Application.Validators.Commands
{
    public class CreateRefreshTokenCommandValidator : AbstractValidator<CreateRefreshTokenCommand>
    {
        public CreateRefreshTokenCommandValidator()
        {
            RuleFor(r => r.Token).NotEmpty();
            RuleFor(r => r.UserId).NotEmpty();
            RuleFor(r => r.ExpirationTime).NotEmpty().GreaterThan(DateTime.UtcNow);
        }
    }
}
