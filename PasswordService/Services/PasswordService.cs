using FluentValidation;
using Grpc.Core;
using PasswordService.Enums.EventId;
using PasswordService.Interfaces.Repositories;
using PasswordService.Model.Entities;
using RpcExceptionHandlersLib;

namespace PasswordService.Services
{
    public class PasswordService : PasswordProtoService.PasswordProtoServiceBase
    {
        private readonly ILogger<PasswordService> _logger;
        private readonly IPasswordRepository _passwordRepository;
        
        public PasswordService(ILogger<PasswordService> logger, IPasswordRepository passwordRepository)
        {
            _logger = logger;
            _passwordRepository = passwordRepository;
        }

        public override async Task<PasswordModel> CreatePassword(CreatePasswordRequest request, ServerCallContext context)
        {
            PasswordModel reply = new();

            try
            {
                Password password = new(request.UserId, request.Title, request.Login, request.PasswordValue, request.Commentary, request.Image);
                await _passwordRepository.AddAsync(password);

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
                var rpcEx = RpcExceptionHandler.HandleException(ex);
                _logger.LogError(new EventId((int)rpcEx.StatusCode, nameof(rpcEx.StatusCode)), ex, "Invalid password arguments");
                throw rpcEx;        
            }
            catch (Exception ex)
            {
                var rpcEx = RpcExceptionHandler.HandleException(ex);
                _logger.LogError(new EventId((int)rpcEx.StatusCode, nameof(rpcEx.StatusCode)), ex, rpcEx.Message);
                throw rpcEx;
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
                await _passwordRepository.UpdateAsync(password);

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
                var rpcEx = RpcExceptionHandler.HandleException(ex);
                _logger.LogError(new EventId((int)rpcEx.StatusCode, nameof(rpcEx.StatusCode)), ex, "Invalid password arguments");
                throw rpcEx;
            }
            catch (Exception ex)
            {
                var rpcEx = RpcExceptionHandler.HandleException(ex);
                _logger.LogError(new EventId((int)rpcEx.StatusCode, nameof(rpcEx.StatusCode)), ex, rpcEx.Message);
                throw rpcEx;
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
                _logger.LogInformation($"{password.Login}");
                _logger.LogInformation($"{password.PasswordValue}");
                _logger.LogInformation($"{password.Commentary}");
                await _passwordRepository.DeleteAsync(password.Id);

                reply.Id = password.Id;
                reply.UserId = password.UserId;
                reply.Title = password.Title;
                reply.Login = password.Login;
                reply.PasswordValue = password.PasswordValue;
                reply.Commentary = password.Commentary;
                reply.Image = password.Image;
            }
            catch (RpcException ex)
            {
                _logger.LogError(new EventId((int)ex.StatusCode, nameof(ex.StatusCode)), ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                var rpcEx = RpcExceptionHandler.HandleException(ex);
                _logger.LogError(new EventId((int)rpcEx.StatusCode, nameof(rpcEx.StatusCode)), ex, rpcEx.Message);
                throw rpcEx;
            }

            _logger.LogInformation(new EventId((int)PasswordEvent.DeletePassword, nameof(DeletePassword)), $"Password for id:{reply.Id} has been deleted");
            return reply;
        }
        
        public override async Task<PasswordModels> GetPasswordsByUserId(GetPasswordsByUserIdRequest request, ServerCallContext context)
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
                var rpcEx = RpcExceptionHandler.HandleException(ex);
                _logger.LogError(new EventId((int)rpcEx.StatusCode, nameof(rpcEx.StatusCode)), ex, rpcEx.Message);
                throw rpcEx;
            }

            _logger.LogInformation(new EventId((int)PasswordEvent.GetPasswordsBy, nameof(GetPasswordsByUserId)), $"Passwords for user {request.UserId} were successfully received");
            return reply;
        }
    }
}
