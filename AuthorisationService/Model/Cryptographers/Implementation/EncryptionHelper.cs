namespace AuthorisationService.Model.Cryptographers.Implementation
{
    public class EncryptionHelper : IEncryptionHelper
    {
        public async Task DecryptAsync<TEntity>(IEncryptor encryptor, TEntity entity)
        {
            foreach (var property in typeof(TEntity).GetProperties())
            {
                if (property.PropertyType != typeof(string) || string.IsNullOrEmpty((string?)property.GetValue(entity)))
                    continue;

                property.SetValue(entity, await encryptor.DecryptAsync((string)property.GetValue(entity)));
            }
        }

        public async Task EncryptAsync<TEntity>(IEncryptor encryptor, TEntity entity)
        {
            foreach (var property in typeof(TEntity).GetProperties())
            {
                if (property.PropertyType != typeof(string) || string.IsNullOrEmpty((string?)property.GetValue(entity)))
                    continue;

                property.SetValue(entity, await encryptor.EncryptAsync((string)property.GetValue(entity)));
            }
        }
    }
}
