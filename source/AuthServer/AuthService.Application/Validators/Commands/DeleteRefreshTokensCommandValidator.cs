using AuthService.Application.CQRS.Commands.DeleteRefreshTokens;
using FluentValidation;

namespace AuthService.Application.Validators.Commands
{
    public class DeleteRefreshTokensCommandValidator : AbstractValidator<DeleteRefreshTokensCommand>
    {
        public DeleteRefreshTokensCommandValidator()
        {
            RuleFor(r => r.UserId).NotEmpty();
        }
    }
}
