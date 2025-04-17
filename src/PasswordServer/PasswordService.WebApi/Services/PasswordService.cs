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
        private readonly IMessageBus _messageBus;
        private readonly IPasswordDTOMapper _passwordDTOMapper;

        public PasswordService(IMessageBus messageBus, IPasswordDTOMapper passwordDTOMapper)
        {
            _messageBus = messageBus;
            _passwordDTOMapper = passwordDTOMapper;
        }

        [Authorize]
        public override async Task<CreatePasswordResponse> CreatePassword(CreatePasswordRequest request, ServerCallContext context)
        {
            CancellationToken cancellation = context.CancellationToken;
            cancellation.ThrowIfCancellationRequested();

            var user = context.GetHttpContext().User;
            Guid userId = Guid.Parse(user.Claims.First().Value);

            CreatePasswordCommand command = request.ToCommand(userId);
            Guid passwordId = await _messageBus.InvokeAsync<Guid>(command, cancellation);

            CreatePasswordResponse response = new() { PasswordId = passwordId.ToString() };    
            return response;
        }

        [Authorize]
        public override async Task<UpdatePasswordResponse> UpdatePassword(UpdatePasswordRequest request, ServerCallContext context)
        {
            CancellationToken cancellation = context.CancellationToken;
            cancellation.ThrowIfCancellationRequested();

            var user = context.GetHttpContext().User;
            Guid userId = Guid.Parse(user.Claims.First().Value);

            UpdatePasswordCommand command = request.ToCommand(userId);
            Guid passwordId = await _messageBus.InvokeAsync<Guid>(command, cancellation);

            UpdatePasswordResponse response = new() { PasswordId = passwordId.ToString() };
            return response;
        }

        [Authorize]
        public override async Task<DeletePasswordResponse> DeletePassword(DeletePasswordRequest request, ServerCallContext context)
        {
            CancellationToken cancellation = context.CancellationToken;
            cancellation.ThrowIfCancellationRequested();

            DeletePasswordCommand command = request.ToCommand();
            Guid passwordId = await _messageBus.InvokeAsync<Guid>(command, cancellation);

            DeletePasswordResponse response = new() { PasswordId = passwordId.ToString() };
            return response;
        }

        [Authorize]
        public override async Task<GetPasswordsResponse> GetPasswords(GetPasswordsRequest request, ServerCallContext context)
        {
            CancellationToken cancellation = context.CancellationToken;
            cancellation.ThrowIfCancellationRequested();

            var user = context.GetHttpContext().User;
            Guid userId = Guid.Parse(user.Claims.First().Value);

            GetPasswordsByUserIDQuery query = new(userId);
            IEnumerable<PasswordDTO> passwords = await _messageBus.InvokeAsync<IEnumerable<PasswordDTO>>(query, cancellation);

            GetPasswordsResponse response = new();
            response.Passwords.AddRange(passwords.Select(_passwordDTOMapper.ToPasswordResponse));
            return response;
        }
    }
}
