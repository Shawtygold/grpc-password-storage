using AuthService.Model.Entities;
using FluentValidation;

namespace AuthService.Model.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        private const int loginMinLength = 2;
        private const int loginMaxLength = 50;
        private const int passwordMinLength = 4;
        private const int passwordMaxLength = 100;

        public UserValidator()
        {
            RuleFor(u => u.Id).NotNull().GreaterThan(-1);
            RuleFor(u => u.Login).NotNull().MinimumLength(loginMinLength).MaximumLength(loginMaxLength);
            RuleFor(u => u.Password).NotNull().MinimumLength(passwordMinLength).MaximumLength(passwordMaxLength);
        }
    }
}
