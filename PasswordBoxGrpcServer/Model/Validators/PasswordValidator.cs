using FluentValidation;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Model.Validators
{
    public class PasswordValidator : AbstractValidator<Password>
    {
        public PasswordValidator()
        {
            RuleFor(p => p.Id).NotNull().GreaterThan(-1);
            RuleFor(p => p.UserLogin).NotNull();
            RuleFor(p => p.Title).NotNull();
            RuleFor(p => p.Login).NotNull();
            RuleFor(p => p.PasswordValue).NotNull();
            RuleFor(p => p.Image).NotNull();
        }
    }
}
