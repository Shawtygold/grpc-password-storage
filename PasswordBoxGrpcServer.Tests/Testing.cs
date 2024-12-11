using AESEncryptionLib.Model;
using AuthorisationServiceTest;
using Grpc.Net.Client;
using PasswordService.Interfaces.Cryptographers;
using PasswordService.Model;
using PasswordService.Model.Cryptographers;
using PasswordService.Model.Cryptographers.Configs;
using PasswordServiceTest;
using Xunit;


namespace PasswordBoxGrpcServer.Tests
{
    public class Testing
    {
        [Fact]
        public void AesConfigResultNotNull()
        {
            //Act
            var config = JSONReader.ReadAesConfig("config.json");

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
            IEncryptor encryptor = new AesEncryptor(new AES(), new AesConfig());
            string data = "Admin";

            //Act
            string encryptedData = await encryptor.EncryptAsync(data);
            string decryptedData = await encryptor.DecryptAsync(encryptedData);

            //Assert
            Assert.Equal(data, decryptedData);
        }

        [Fact]
        public async Task CreatePassword()
        {
            //Arrange
            var channel = GrpcChannel.ForAddress("http://localhost:5159");
            var client = new PasswordProtoService.PasswordProtoServiceClient(channel);
            CreatePasswordRequest createPasswordRequest = new()
            {
                Login = "GGmail",
                PasswordValue = "password",
                Image = "ы",
                Title = "Gmail",
                UserLogin = "Vital"
            };

            //Act
            var respone = await client.CreatePasswordAsync(createPasswordRequest);

            //Assert
            Assert.Equal(createPasswordRequest.Title, respone.Title);
        }

        [Fact]
        public async Task ReegisterUser()
        {
            //Arrange
            var channel = GrpcChannel.ForAddress("http://localhost:5161");
            var client = new AuthorisationProtoService.AuthorisationProtoServiceClient(channel);
            RegisterUserRequest registerUserRequest = new()
            {
                Login = "Adminka",
                Passsword = "Adminka123"
            };

            //Act
            var reply = client.RegisterUserAsync(registerUserRequest);

            //Assert
            //Assert.Equal(,)
        }
    }
}
