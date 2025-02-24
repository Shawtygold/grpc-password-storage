using FluentValidation;
using PasswordService.Model.Entities;

namespace PasswordService.Model.Validators
{
    public class PasswordValidator : AbstractValidator<Password>
    {
        private const int MAX_TITLE_LENGTH = 100;
        private const int MAX_LOGIN_LENGTH = 100;
        public PasswordValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("User ID must not be empty")
                .GreaterThanOrEqualTo(0).WithMessage("User ID must be less than or equal to 0");

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title must not be empty")
                .MaximumLength(MAX_TITLE_LENGTH).WithMessage($"Title must be less than {MAX_TITLE_LENGTH} characters");

            RuleFor(p => p.Login)
                .NotEmpty().WithMessage("Login must not be empty")
                .MaximumLength(MAX_LOGIN_LENGTH).WithMessage($"Login must be less than {MAX_LOGIN_LENGTH} characters");

            RuleFor(p => p.EncryptedPassword)
                .NotEmpty().WithMessage("Password must not be empty");

            RuleFor(p => p.IconPath)
                .NotEmpty().WithMessage("Image source must not be empty");
        }
    }
}
