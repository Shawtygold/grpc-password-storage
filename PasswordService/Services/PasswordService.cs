using FluentValidation;
using Grpc.Core;
using PasswordService.Interfaces.Cryptographers;
using PasswordService.Interfaces.Repositories;
using PasswordService.Model.Entities;
using RpcExceptionHandlersLib;

namespace PasswordService.Services
{
    public class PasswordService : PasswordProtoService.PasswordProtoServiceBase
    {
        private readonly ILogger<PasswordService> _logger;
        private readonly IPasswordRepository _passwordRepository;
        private readonly IEncryptor _encryptor;
        
        public PasswordService(ILogger<PasswordService> logger, IPasswordRepository passwordRepository, IEncryptor encryptor)
        {
            _logger = logger;
            _passwordRepository = passwordRepository;
            _encryptor = encryptor;
        }

        public override async Task<PasswordModel> CreatePassword(CreatePasswordRequest request, ServerCallContext context)
        {
            PasswordModel reply = new();

            try
            {
                Password password = new(request.UserLogin, request.Title, request.Login, request.PasswordValue, request.Commentary, request.Image);
                await EncryptPasswordAsync(password);
                await _passwordRepository.AddAsync(password);
                await DecryptPasswordAsync(password);

                reply.Id = password.Id;
                reply.UserLogin = password.UserLogin;
                reply.Title = password.Title;
                reply.Login = password.Login;
                reply.PasswordValue = password.PasswordValue;
                reply.Commentary = password.Commentary;
                reply.Image = password.Image;
            }
            catch (ValidationException ex)
            {
                _logger.LogError((int)StatusCode.InvalidArgument, ex, "Invalid password arguments");
                RpcExceptionThrower.Handle(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)StatusCode.Internal, ex, "Internal Error");
                RpcExceptionThrower.Handle(ex);
            }

            return reply;
        }

        public override async Task<PasswordModel> UpdatePassword(UpdatePasswordRequest request, ServerCallContext context)
        {
            PasswordModel reply = new();

            try
            {
                Password password = new(request.Password.Id, request.Password.UserLogin, request.Password.Title, request.Password.Login, request.Password.PasswordValue, request.Password.Commentary, request.Password.Image);
                await EncryptPasswordAsync(password);
                await _passwordRepository.UpdateAsync(password);
                await DecryptPasswordAsync(password);

                reply.Id = password.Id;
                reply.UserLogin = password.UserLogin;
                reply.Title = password.Title;
                reply.Login = password.Login;
                reply.PasswordValue = password.PasswordValue;
                reply.Commentary = password.Commentary;
                reply.Image = password.Image;
            }
            catch (ValidationException ex)
            {
                _logger.LogError((int)StatusCode.InvalidArgument, ex, "Invalid password arguments");
                RpcExceptionThrower.Handle(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)StatusCode.Internal, ex, "Internal Error");
                RpcExceptionThrower.Handle(ex);
            }

            return reply;
        }

        public override async Task<PasswordModel> DeletePassword(DeletePasswordRequest request, ServerCallContext context)
        {
            PasswordModel reply = new();

            try
            {
                Password? password = await _passwordRepository.GetByAsync(p => p.Id == request.Id)
                    ?? throw new RpcException(new Grpc.Core.Status(StatusCode.NotFound, "Password with this ID does not exist"));
                
                await _passwordRepository.DeleteAsync(password.Id);

                reply.Id = password.Id;
                reply.UserLogin = password.UserLogin;
                reply.Title = password.Title;
                reply.Login = password.Login;
                reply.PasswordValue = password.PasswordValue;
                reply.Commentary = password.Commentary;
                reply.Image = password.Image;
            }
            catch (Exception ex)
            {
                _logger.LogError((int)StatusCode.Internal, ex, "Internal Error");
                RpcExceptionThrower.Handle(ex);
            }

            return reply;
        }

        public override async Task<GetPasswordsReply> GetPasswords(GetPasswordsRequest request, ServerCallContext context)
        {
            List<Password> passwords;
            GetPasswordsReply reply = new();

            try
            {
                string encryptedUserLogin = await _encryptor.EncryptAsync(request.UserLogin);
                passwords = new(await _passwordRepository.GetCollectionBy(p => p.UserLogin == encryptedUserLogin));

                var passwordList = passwords.Select(item => new PasswordModel()
                {
                    Id = item.Id,
                    UserLogin = item.UserLogin,
                    Title = item.Title,
                    Login = item.Login,
                    PasswordValue = item.PasswordValue,
                    Commentary = item.Commentary,
                    Image = item.Image
                }).ToList();

                reply.Passwords.AddRange(passwordList);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)StatusCode.Internal, ex, "Internal Error");
                RpcExceptionThrower.Handle(ex);
            }

            return reply;
        }

        private async Task EncryptPasswordAsync(Password password)
        {
            foreach (var property in typeof(Password).GetProperties())
            {
                if (property.PropertyType != typeof(string) || string.IsNullOrEmpty((string?)property.GetValue(password)))
                    continue;

                property.SetValue(password, await _encryptor.EncryptAsync((string)property.GetValue(password)));
            }
        }
        private async Task DecryptPasswordAsync(Password password)
        {
            foreach (var property in typeof(Password).GetProperties())
            {
                if (property.PropertyType != typeof(string) || string.IsNullOrEmpty((string?)property.GetValue(password)))
                    continue;

                property.SetValue(password, await _encryptor.DecryptAsync((string)property.GetValue(password)));
            }
        }
    }
}
