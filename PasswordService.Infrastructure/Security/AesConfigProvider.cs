using FluentValidation;
using Microsoft.Extensions.Options;
using PasswordService.Infrastructure.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace PasswordService.Infrastructure.Security
{
    public class AesConfigProvider : IAesConfigProvider
    {
        private readonly IOptions<AesSettings> _settings;
        private readonly IValidator<AesEncryptionConfig> _validator;

        public AesConfigProvider(IOptions<AesSettings> settings, IValidator<AesEncryptionConfig> validator)
        {
            _settings = settings;
            _validator = validator;
        }

        public AesEncryptionConfig GetAesConfig()
        {
            var aesConfig = new AesEncryptionConfig(
                Encoding.Default.GetBytes(_settings.Value.Key),
                Encoding.Default.GetBytes(_settings.Value.IV),
                (PaddingMode)int.Parse(_settings.Value.PaddingMode),
                (CipherMode)int.Parse(_settings.Value.CipherMode));

            _validator.ValidateAndThrow(aesConfig);

            return aesConfig;
        }
    }
}
