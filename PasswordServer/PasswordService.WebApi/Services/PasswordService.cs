using FluentValidation;
using Grpc.Core;
using GrpcPasswordService;
using Marten;
using Microsoft.AspNetCore.Authorization;
using PasswordService.Application.CQRS.Commands.CreatePassword;
using PasswordService.Application.CQRS.Commands.DeletePassword;
using PasswordService.Application.CQRS.Commands.UpdatePassword;
using PasswordService.Application.CQRS.Queries.GetPasswordsByUserID;
using PasswordService.Application.DTO;
using PasswordService.Application.Exceptions;
using PasswordService.WebApi.Exceptions;
using PasswordService.WebApi.Extensions;
using Wolverine;

namespace PasswordService.WebApi.Services
{
    public class PasswordService : PasswordProtoService.PasswordProtoServiceBase
    {
        private readonly ILogger<PasswordService> _logger;
        private readonly IMessageBus _messageBus;
        private static readonly string _domain = AppDomain.CurrentDomain.FriendlyName;

        public PasswordService(ILogger<PasswordService> logger, IMessageBus messageBus)
        {
            _logger = logger;
            _messageBus = messageBus;
        }

        [Authorize]
        public override async Task<CreatePasswordResponse> CreatePassword(CreatePasswordRequest request, ServerCallContext context)
        {
            var user = context.GetHttpContext().User;
            Guid userId = Guid.Parse(user.Claims.First().Value);

            CreatePasswordResponse response;
            try
            {
                CreatePasswordCommand command = request.ToCommand(userId);
                Guid passwordId = await _messageBus.InvokeAsync<Guid>(command);

                response = new() { PasswordId = passwordId.ToString() };        
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning("{Date} Validation failed for {Command}", DateTime.Now, typeof(CreatePasswordCommand).Name);
                throw PasswordRpcExceptions.InvalidArgumets(ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Date} {Operation} Failure \"{Message}\"", DateTime.Now, nameof(CreatePassword), ex.Message);
                throw PasswordRpcExceptions.InternalError(_domain, ex);
            }

            _logger.LogInformation("{Date} {Operation} Password with Id '{PasswordId}' has been created", DateTime.Now, nameof(CreatePassword), response.PasswordId);
            return response;
        }

        [Authorize]
        public override async Task<UpdatePasswordResponse> UpdatePassword(UpdatePasswordRequest request, ServerCallContext context)     
        {
            var user = context.GetHttpContext().User;
            Guid userId = Guid.Parse(user.Claims.First().Value);

            UpdatePasswordResponse response;
            try
            {
                UpdatePasswordCommand command = request.ToCommand(userId);
                Guid passwordId = await _messageBus.InvokeAsync<Guid>(command);

                response = new() { PasswordId = passwordId.ToString() };
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning("{Date} Validation failed for {Command}", DateTime.Now, typeof(CreatePasswordCommand).Name);
                throw PasswordRpcExceptions.InvalidArgumets(ex.Errors);    
            }
            catch (PasswordNotFoundException ex)
            {
                _logger.LogError("{Date} {Operation} Failure \"{Message}\"", DateTime.Now, nameof(UpdatePassword), ex.Message);
                throw PasswordRpcExceptions.NotFound(_domain, request.Password.Id);
            }
            catch (Exception ex)
            {
                 _logger.LogError("{Date} {Operation} Failure \"{Message}\"", DateTime.Now, nameof(UpdatePassword), ex.Message);
                throw PasswordRpcExceptions.InternalError(_domain, ex);
            }

            _logger.LogInformation("{Date} {Operation} Password with Id '{PasswordId} has been updated", DateTime.Now, nameof(UpdatePassword), response.PasswordId);
            return response;
        }

        [Authorize]
        public override async Task<DeletePasswordResponse> DeletePassword(DeletePasswordRequest request, ServerCallContext context)
        {
            DeletePasswordResponse response;

            try
            {
                DeletePasswordCommand command = request.ToCommand();
                Guid passwordId = await _messageBus.InvokeAsync<Guid>(command);

                response = new() { PasswordId = passwordId.ToString() };
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning("{Date} Validation failed for {Command}", DateTime.Now, typeof(CreatePasswordCommand).Name);
                throw PasswordRpcExceptions.InvalidArgumets(ex.Errors);
            }
            catch (PasswordNotFoundException ex)
            {
                _logger.LogError("{Date} {Operation} Failure \"{Message}\"", DateTime.Now, nameof(UpdatePassword), ex.Message);
                throw PasswordRpcExceptions.NotFound(_domain, request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Date} {Operation} Failure \"{Message}\"", DateTime.Now, nameof(DeletePassword), ex.Message);
                throw PasswordRpcExceptions.InternalError(_domain, ex);
            }

            _logger.LogInformation("{Date} {Operation} Password with Id '{PasswordId}' has been deleted", DateTime.Now, nameof(DeletePassword), request.Id);
            return response;
        }

        [Authorize]
        public override async Task<GetPasswordsResponse> GetPasswords(GetPasswordsRequest request, ServerCallContext context)
        {
            var user = context.GetHttpContext().User;
            Guid userId = Guid.Parse(user.Claims.First().Value);

            GetPasswordsResponse response = new();

            try
            {
                GetPasswordsByUserIDQuery query = new(userId);
                IEnumerable<PasswordDTO> passwords = await _messageBus.InvokeAsync<IEnumerable<PasswordDTO>>(query);

                response.Passwords.AddRange(passwords.Select(p => p.ToPasswordResponse()));
            }
            catch (ValidationException ex)
            {
                throw PasswordRpcExceptions.InvalidArgumets(ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Date} {Operation} Failure \"{Message}\"", DateTime.Now, nameof(GetPasswords), ex.Message);
                throw PasswordRpcExceptions.InternalError(_domain, ex);
            }
         
            return response;
        }
    }
}
