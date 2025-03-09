using FluentValidation;
using PasswordService.Infrastructure.Security;

namespace PasswordService.Infrastructure.Validators
{
    public class AesEncryptionConfigValidator : AbstractValidator<AesEncryptionConfig>
    {
        public AesEncryptionConfigValidator()
        {
            RuleFor(c => c.Key).NotEmpty();
            RuleFor(c => c.IV).NotEmpty();
            RuleFor(c => (int)c.PaddingMode).NotEmpty().InclusiveBetween(1, 5);
            RuleFor(c => (int)c.CipherMode).NotEmpty().InclusiveBetween(1, 5);
        }
    }
}
