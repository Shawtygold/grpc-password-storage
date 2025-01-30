namespace PasswordService.Model.Cryptographers
{
    public interface IEncryptionHelper
    {
        public Task EncryptAsync<TEntity>(IEncryptor encryptor, TEntity entity);
        public Task DecryptAsync<TEntity>(IEncryptor encryptor, TEntity entity);
    }
}
