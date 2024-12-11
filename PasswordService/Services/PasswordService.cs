using FluentValidation;
using Grpc.Core;
using PasswordService.Enums.EventId;
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
        private readonly IEncryptionHelper _encryptionHelper;
        
        public PasswordService(ILogger<PasswordService> logger, IPasswordRepository passwordRepository, IEncryptor encryptor, IEncryptionHelper encryptionHelper)
        {
            _logger = logger;
            _passwordRepository = passwordRepository;
            _encryptor = encryptor;
            _encryptionHelper = encryptionHelper;
        }

        public override async Task<PasswordModel> CreatePassword(CreatePasswordRequest request, ServerCallContext context)
        {
            PasswordModel reply = new();

            try
            {
                Password password = new(request.UserId, request.Title, request.Login, request.PasswordValue, request.Commentary, request.Image);
                await _encryptionHelper.EncryptAsync(_encryptor, password);
                await _passwordRepository.AddAsync(password);
                await _encryptionHelper.DecryptAsync(_encryptor, password);

                reply.Id = password.Id;
                reply.UserId = password.UserId;
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

            _logger.LogInformation(new EventId((int)PasswordEvent.CreatePassword, nameof(CreatePassword)), $"Password for userId {reply.UserId} has been created | Password Id:{reply.Id}");
            return reply;
        }

        public override async Task<PasswordModel> UpdatePassword(UpdatePasswordRequest request, ServerCallContext context)
        {
            PasswordModel reply = new();

            try
            {
                Password password = new(request.Password.Id, request.Password.UserId, request.Password.Title, request.Password.Login, request.Password.PasswordValue, request.Password.Commentary, request.Password.Image);
                await _encryptionHelper.EncryptAsync(_encryptor, password);
                await _passwordRepository.UpdateAsync(password);
                await _encryptionHelper.DecryptAsync(_encryptor, password);

                reply.Id = password.Id;
                reply.UserId = password.UserId;
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

            _logger.LogInformation(new EventId((int)PasswordEvent.UpdatePassword, nameof(UpdatePassword)), $"Password for userId {reply.UserId} has been updated | Password Id:{reply.Id}");
            return reply;
        }

        public override async Task<PasswordModel> DeletePassword(DeletePasswordRequest request, ServerCallContext context)
        {
            PasswordModel reply = new();

            try
            {
                Password? password = await _passwordRepository.GetByIDAsync(request.Id)
                    ?? throw new RpcException(new Status(StatusCode.NotFound, "Password with this ID does not exist"));
                
                await _passwordRepository.DeleteAsync(password.Id);
                await _encryptionHelper.DecryptAsync(_encryptor, password);

                reply.Id = password.Id;
                reply.UserId = password.UserId;
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

            _logger.LogInformation(new EventId((int)PasswordEvent.DeletePassword, nameof(DeletePassword)), $"Password for id:{reply.Id} has been deleted");
            return reply;
        }
        
        public override async Task<PasswordModels> GetPasswordsBy(GetPasswordsByRequest request, ServerCallContext context)
        {
            List<Password> passwords;
            PasswordModels reply = new();

            try
            {
                passwords = new(await _passwordRepository.GetCollectionByAsync(p => p.UserId == request.UserId));

                var passwordList = passwords.Select(item => new PasswordModel()
                {
                    Id = item.Id,
                    UserId = item.UserId,
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

            _logger.LogInformation(new EventId((int)PasswordEvent.GetPasswordsBy, nameof(GetPasswordsBy)), $"Passwords for user {request.UserId} were successfully received");
            return reply;
        }
    }
}
