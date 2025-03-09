using PasswordService.Infrastructure.Security;

namespace PasswordService.Infrastructure.Abstractions
{
    public interface IAesConfigProvider
    {
        public AesEncryptionConfig GetAesConfig();
    }
}
