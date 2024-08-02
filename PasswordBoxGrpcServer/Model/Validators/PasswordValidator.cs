using FluentValidation;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Model.Validators
{
    public class PasswordValidator : AbstractValidator<Password>
    {
        private const int maxLoginLength = 100;

        public PasswordValidator()
        {
            RuleFor(p => p.Id).NotNull().GreaterThan(-1);
            RuleFor(p => p.Title).NotNull();
            RuleFor(p => p.Login).NotNull().MaximumLength(maxLoginLength);
            RuleFor(p => p.PasswordHash).NotNull();
            RuleFor(p => p.Image).NotNull();
        }
    }
}
