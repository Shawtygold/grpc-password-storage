using FluentValidation;
using Grpc.Core;
using GrpcPasswordService;
using MediatR;
using Microsoft.Extensions.Logging;
using PasswordService.Application.CQRS.Commands.CreatePassword;
using PasswordService.Application.CQRS.Commands.DeletePassword;
using PasswordService.Application.CQRS.Commands.UpdatePassword;
using PasswordService.Application.CQRS.Queries.GetPasswordsByUserID;
using PasswordService.Application.Exceptions;
using PasswordService.Presentation.Exceptions;
using PasswordService.Presentation.Extensions;
using PasswordService.Presentation.Mappers;

namespace PasswordService.Presentation.GRPC.Implementation
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
                response = PasswordResponseMapper.ToCreatePasswordResponse(await _mediator.Send(command));             
            }
            catch (ValidationException ex)
            {
                var rpcEx = PasswordRpcExceptions.InvalidArgumets(ex.Errors);
                throw rpcEx;
            }
            catch (Exception ex)
            {
                var rpcEx = PasswordRpcExceptions.InternalError(_domain, ex);
                _logger.LogError("{Date} {Operation} Failure \"{Message}\"", DateTime.Now, nameof(CreatePassword), ex.Message);
                throw rpcEx;
            }

            _logger.LogInformation("{Date} {Operation} Password for UserId {UserId} has been created", DateTime.Now, nameof(CreatePassword), request.UserId);
            return response;
        }

        public override async Task<UpdatePasswordResponse> UpdatePassword(UpdatePasswordRequest request, ServerCallContext context)
        {
            UpdatePasswordResponse response;

            try
            {
                UpdatePasswordCommand command = request.ToCommand();
                response = PasswordResponseMapper.ToUpdatePasswordResponse(await _mediator.Send(command));
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
                 _logger.LogError("{Date} {Operation} Failure \"{Message}\"", DateTime.Now, nameof(UpdatePassword), ex.Message);
                throw rpcEx;
            }

            _logger.LogInformation("{Date} {Operation} Password for UserId {UserId} has been updated", DateTime.Now, nameof(UpdatePassword), request.Password.UserId);
            return response;
        }

        public override async Task<DeletePasswordResponse> DeletePassword(DeletePasswordRequest request, ServerCallContext context)
        {
            DeletePasswordResponse response;

            try
            {
                DeletePasswordCommand command = request.ToCommand();
                response = PasswordResponseMapper.ToDeletePasswordResponse(await _mediator.Send(command));
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
                _logger.LogError("{Date} {Operation} Failure \"{Message}\"", DateTime.Now, nameof(DeletePassword), ex.Message);
                throw rpcEx;
            }

            _logger.LogInformation("{Date} {Operation} Password with Id {PasswordId} has been deleted", DateTime.Now, nameof(DeletePassword), request.Id);
            return response;
        }

        public override async Task<GetPasswordsResponse> GetPasswordsByUserId(GetPasswordsByUserIdRequest request, ServerCallContext context)
        {
            GetPasswordsResponse response = new();

            try
            {
                GetPasswordsByUserIDQuery query = request.ToQuery();
                response.Passwords.AddRange((await _mediator.Send(query)).ToList().Select(PasswordResponseMapper.ToPasswordModel));
            }
            catch (ValidationException ex)
            {
                throw PasswordRpcExceptions.InvalidArgumets(ex.Errors);
            }
            catch (Exception ex)
            {
                var rpcEx = PasswordRpcExceptions.InternalError(_domain, ex);
                _logger.LogError("{Date} {Operation} Failure \"{Message}\"", DateTime.Now, nameof(CreatePassword), ex.Message);
                throw rpcEx;
            }

            _logger.LogInformation("{Date} {Operation} Passwords for UserId {UserId} has been retrieved", DateTime.Now, nameof(GetPasswordsByUserId), request.UserId);           
            return response;
        }
    }
}
