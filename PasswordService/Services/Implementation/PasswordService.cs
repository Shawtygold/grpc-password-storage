using FluentValidation;
using Grpc.Core;
using GrpcPasswordService;
using MediatR;
using PasswordService.Model.CQRS.Commands;
using PasswordService.Model.CQRS.Queries;
using PasswordService.Model.Exceptions;
using PasswordService.Model.Extensions;

namespace PasswordService.Services.Implementation
{
    public class PasswordService : PasswordProtoService.PasswordProtoServiceBase, IPasswordService
    {
        private readonly ILogger<PasswordService> _logger;
        private readonly IMediator _mediator;
        private static readonly string _domain = AppDomain.CurrentDomain.FriendlyName;

        public PasswordService(ILogger<PasswordService> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public override async Task<CreatePasswordResponse> CreatePassword(CreatePasswordRequest request, ServerCallContext context)
        {
            CreatePasswordResponse response;
            try
            {
                CreatePasswordCommand command = request.ToCommand();
                response = await _mediator.Send(command);
            }
            catch (ValidationException ex)
            {
                var rpcEx = PasswordRpcExceptions.InvalidArgumets(ex.Errors);
                throw rpcEx;
            }
            catch (Exception ex)
            {
                var rpcEx = PasswordRpcExceptions.InternalError(_domain, ex);
                throw rpcEx;
            }

            _logger.LogInformation($"{DateTime.Now} {nameof(CreatePassword)} Password for UserId {request.UserId} has been created");
            return response;
        }

        public override async Task<UpdatePasswordResponse> UpdatePassword(UpdatePasswordRequest request, ServerCallContext context)
        {
            UpdatePasswordResponse response;

            try
            {
                UpdatePasswordCommand command = request.ToCommand();
                response = await _mediator.Send(command);
            }
            catch (ValidationException ex)
            {
                var rpcEx = PasswordRpcExceptions.InvalidArgumets(ex.Errors);
                throw rpcEx;
            }
            catch (PasswordNotFoundException)
            {
                var rpcEx = PasswordRpcExceptions.NotFound(_domain, request.Password.Id);
                throw rpcEx;
            }
            catch (Exception ex)
            {
                var rpcEx = PasswordRpcExceptions.InternalError(_domain, ex);
                throw rpcEx;
            }

            _logger.LogInformation($"{DateTime.Now} {nameof(UpdatePassword)} Password for UserId {response.Password.UserId} has been updated");
            return response;
        }

        public override async Task<DeletePasswordResponse> DeletePassword(DeletePasswordRequest request, ServerCallContext context)
        {
            DeletePasswordResponse response;

            try
            {
                DeletePasswordCommand command = request.ToCommand();
                response = await _mediator.Send(command);
            }
            catch (ValidationException ex)
            {
                var rpcEx = PasswordRpcExceptions.InvalidArgumets(ex.Errors);
                throw rpcEx;
            }
            catch (PasswordNotFoundException)
            {
                var rpcEx = PasswordRpcExceptions.NotFound(_domain, request.Id);
                throw rpcEx;
            }
            catch (Exception ex)
            {
                var rpcEx = PasswordRpcExceptions.InternalError(_domain, ex);
                throw rpcEx;
            }

            _logger.LogInformation($"{DateTime.Now} {nameof(DeletePassword)} Password with Id {request.Id} has been deleted");
            return response;
        }

        public override async Task<GetPasswordsResponse> GetPasswordsByUserId(GetPasswordsByUserIdRequest request, ServerCallContext context)
        {
            GetPasswordsResponse response;

            try
            {
                GetPasswordsByUserIDQuery query = request.ToQuery();
                response = await _mediator.Send(query);
            }
            catch (Exception ex)
            {
                var rpcEx = PasswordRpcExceptions.InternalError(_domain, ex);
                throw rpcEx;
            }

            _logger.LogInformation($"{DateTime.Now} {nameof(GetPasswordsByUserId)} Passwords for UserId {request.UserId} has been retrieved");           
            return response;
        }
    }
}
