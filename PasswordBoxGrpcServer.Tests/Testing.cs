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
        public async Task RegisterUser()
        {
            //Arrange
            var channel = GrpcChannel.ForAddress("http://localhost:5161");
            var client = new AuthorisationProtoService.AuthorisationProtoServiceClient(channel);
            UserRequest registerUserRequest = new()
            {
                Login = "Adminka",
                Password = "Adminka123"
            };

            //Act
            var reply = await client.RegisterUserAsync(registerUserRequest);

            Assert.True(reply.Login == registerUserRequest.Login && reply.Password == registerUserRequest.Password);
        }

        [Fact]
        public async Task CreatePassword()
        {
            //Arrange
            var channel = GrpcChannel.ForAddress("http://localhost:5159");
            var client = new PasswordProtoService.PasswordProtoServiceClient(channel);
            CreatePasswordRequest createPasswordRequest = new()
            {
                UserId = 2,
                Login = "GGmail",
                PasswordValue = "password",
                Image = "ы",
                Title = "Gmail",
                Commentary = "Commd"
            };

            //Act
            var respone = await client.CreatePasswordAsync(createPasswordRequest);

            //Assert
            Assert.Equal(createPasswordRequest.Title, respone.Title);
        }

        [Fact]
        public async Task UpdatePasswod()
        {
            //Arrange
            var channel = GrpcChannel.ForAddress("http://localhost:5159");
            var client = new PasswordProtoService.PasswordProtoServiceClient(channel);
            UpdatePasswordRequest updatePasswordRequest = new()
            {
                Password = new PasswordModel()
                {
                    Id = 1,
                    Image = "ImageUPD",
                    Login = "LoginUpd",
                    Commentary = "CommUpd",
                    PasswordValue = "ыфывфыв",
                    Title = "Passwordsss",
                    UserId = 2
                }
            };

            //Act
            var reply = await client.UpdatePasswordAsync(updatePasswordRequest);
            
            //Assert
            Assert.True(reply.PasswordValue == updatePasswordRequest.Password.PasswordValue &&
                reply.Id == updatePasswordRequest.Password.Id &&
                reply.Title == updatePasswordRequest.Password.Title &&
                reply.Commentary == updatePasswordRequest.Password.Commentary &&
                reply.UserId == updatePasswordRequest.Password.UserId &&
                reply.Image == updatePasswordRequest.Password.Image &&
                reply.Login == updatePasswordRequest.Password.Login);
        }

        [Fact]
        public async Task DeletePassword()
        {
            //Arrange
            var channel = GrpcChannel.ForAddress("http://localhost:5159");
            var client = new PasswordProtoService.PasswordProtoServiceClient(channel);
            int passworId = 9;

            //Act
            var reply = await client.DeletePasswordAsync(new DeletePasswordRequest() { Id = passworId });

            //Assert
            Assert.Equal(reply.Id, passworId);
        }

        [Fact]
        public void GetPasswords()
        {
            //Arrange
            var channel = GrpcChannel.ForAddress("http://localhost:5159");
            var client = new PasswordProtoService.PasswordProtoServiceClient(channel);
            int userID = 1;

            //Act
            var reply = client.GetPasswordsBy(new GetPasswordsByRequest() { UserId = userID });

            //Assert
            foreach (var password in reply.Passwords)
            {
                Assert.Equal(password.UserId, userID);
            }
        }
    }
}
