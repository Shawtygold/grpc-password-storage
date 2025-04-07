using AuthService.Infrastructure.Security;

namespace AuthService.Infrastructure.Abstractions.Security
{
    public interface IAesConfigProvider
    {
        public AesEncryptionConfig GetAesConfig();
    }
}
