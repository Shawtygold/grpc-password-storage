using Grpc.Core;
using GrpcPasswordService;
using Marten;
using Microsoft.AspNetCore.Authorization;
using PasswordService.Application.CQRS.Commands.CreatePassword;
using PasswordService.Application.CQRS.Commands.DeletePassword;
using PasswordService.Application.CQRS.Commands.UpdatePassword;
using PasswordService.Application.CQRS.Queries.GetPasswordsByUserID;
using PasswordService.Application.DTO;
using PasswordService.WebApi.Abstractions;
using PasswordService.WebApi.Extensions;
using Wolverine;

namespace PasswordService.WebApi.Services
{
    public class PasswordService : PasswordProtoService.PasswordProtoServiceBase
    {
        private readonly ILogger<PasswordService> _logger;
        private readonly IMessageBus _messageBus;
        private readonly IGrpcExceptionMapper _grpcExceptionMapper;
        private static readonly string _domain = "passwords";

        public PasswordService(ILogger<PasswordService> logger, IMessageBus messageBus, IGrpcExceptionMapper grpcExceptionMapper)
        {
            _logger = logger;
            _messageBus = messageBus;
            _grpcExceptionMapper = grpcExceptionMapper;
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
            catch (Exception ex)
            {
                throw _grpcExceptionMapper.MapException(_domain, nameof(CreatePassword), ex);
            }

            _logger.LogInformation("{Date} {Domain} {Operation} {Status} {Message}", DateTime.Now, _domain, nameof(CreatePassword), "Success", $"Password has been created. PasswordId: {response.PasswordId}");
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
            catch (Exception ex)
            {
                throw _grpcExceptionMapper.MapException(_domain, nameof(UpdatePassword), ex);
            }

            _logger.LogInformation("{Date} {Domain} {Operation} {Status} {Message}", DateTime.Now, _domain, nameof(UpdatePassword), "Success", $"Password has been updated. PasswordId: {response.PasswordId}");
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
            catch (Exception ex)
            {
                throw _grpcExceptionMapper.MapException(_domain, nameof(DeletePassword), ex);
            }

            _logger.LogInformation("{Date} {Domain} {Operation} {Status} {Message}", DateTime.Now, _domain, nameof(DeletePassword), "Success", $"Password has been deleted. PasswordId: {request.Id}");
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
            catch (Exception ex)
            {
                throw _grpcExceptionMapper.MapException(_domain, nameof(GetPasswords), ex);
            }

            _logger.LogInformation("{Date} {Domain} {Operation} {Status}", DateTime.Now, _domain, nameof(GetPasswords), "Success");

            return response;
        }
    }
}
