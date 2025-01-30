using FluentValidation;
using PasswordsBunker.MVVM.Model.Entities;

namespace PasswordsBunker.MVVM.Model.Valdators
{
    internal class PasswordValidator : AbstractValidator<Password>
    {
        public PasswordValidator()
        {
            RuleFor(p => p.Id).NotNull().GreaterThan(-1);
        }
    }
}
