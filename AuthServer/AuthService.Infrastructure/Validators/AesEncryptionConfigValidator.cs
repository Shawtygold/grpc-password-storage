using AuthService.Infrastructure.Security;
using FluentValidation;

namespace AuthService.Infrastructure.Validators
{
    public class AesEncryptionConfigValidator : AbstractValidator<AesEncryptionConfig>
    {
        public AesEncryptionConfigValidator()
        {
            RuleFor(c => c.Key).NotEmpty();
            RuleFor(c => c.IV).NotEmpty();
            RuleFor(c => c.CipherMode).NotEmpty();
            RuleFor(c => c.PaddingMode).NotEmpty();
        }
    }
}
