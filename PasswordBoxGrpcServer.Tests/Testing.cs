using Xunit;
using PasswordBoxGrpcServer.Model;
using PasswordBoxGrpcServer.Interfaces.Cryptographers;
using PasswordBoxGrpcServer.Model.Cryptographers;
using PasswordBoxGrpcServer.Model.Cryptographers.Configs;
using PasswordBoxGrpcServer.Services.Main;

namespace PasswordBoxGrpcServer.Tests
{
    public class Testing
    {
        [Fact]
        public void AesConfigResultNotNull()
        {
            //Act
            var config = JSONReader.ReadAesConfig();

            //Assert
            Assert.NotNull(config.IV);
            Assert.NotNull(config.Key);
            Assert.NotNull(config.Padding);
            Assert.NotNull(config.Mode);
        }

        [Fact]
        public async Task AesBeforeEncryptionAndAfterDecryptionEqual()
        {
            //Arrange
            IEncryptor encryptor = new AESEncryptor(new AesConfig());
            string data = "Admin";

            //Act
            string encryptedData = await encryptor.EncryptAsync(data);
            string decryptedData = await encryptor.DecryptAsync(encryptedData);

            //Assert
            Assert.Equal(data, decryptedData);
        }
    }
}
