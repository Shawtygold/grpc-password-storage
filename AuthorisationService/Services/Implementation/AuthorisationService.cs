using AuthorisationService.Enums.EventId;
using AuthorisationService.Model.Entities;
using FluentValidation;
using Grpc.Core;
using RpcExceptionHandlersLib;

namespace AuthorisationService.Services.Implementation
{
    public class AuthorisationService : AuthorisationProtoService.AuthorisationProtoServiceBase
    {
        private readonly ILogger<AuthorisationService> _logger;
        private readonly IUserAuthenticator _userAuthenticator;
        private readonly IUserRegistration _userRegistration;

        public AuthorisationService(ILogger<AuthorisationService> logger, IUserAuthenticator userAuthenticator, IUserRegistration userRegistration)
        {
            _logger = logger;
            _userAuthenticator = userAuthenticator;
            _userRegistration = userRegistration;
        }

        public override async Task<AuthenticateUserReply> AuthenticateUser(UserRequest request, ServerCallContext context)
        {
            AuthenticateUserReply reply = new();
            reply.Success = false;

            try
            {
                User user = new(request.Login, request.Password);
                var dbUser = await _userAuthenticator.AuthenticateAsync(user);

                if (dbUser != null)
                {
                    reply.User = new() { Id = dbUser.Id, Login = dbUser.Login, Password = dbUser.Password };
                    reply.Success = true;
                }
            }
            catch (ValidationException ex)
            {
                var rpcEx = RpcExceptionHandler.HandleException(ex);
                _logger.LogError(new EventId((int)rpcEx.StatusCode, nameof(rpcEx.StatusCode)), "Invalid user arguments: " + ex.Message);
                throw rpcEx;
            }
            catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.NotFound)
            {
                _logger.LogWarning(new EventId((int)ex.StatusCode, nameof(ex.StatusCode)), ex.Message);
                throw;
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

            _logger.LogInformation(new EventId((int)AuthorisationEvent.AuthenticateUser, nameof(AuthenticateUser)), $"User {request.Login} authenticated: {reply.Success.ToString()}");

            return reply;
        }

        public override async Task<UserReply> RegisterUser(UserRequest request, ServerCallContext context)
        {
            UserReply reply = new();

            try
            {
                User user = new(request.Login, request.Password);
                User registeredUser = await _userRegistration.RegisterAsync(user);
                reply = new UserReply() { Id = registeredUser.Id, Login = registeredUser.Login, Password = registeredUser.Password };
            }
            catch (ValidationException ex)
            {
                var rpcEx = RpcExceptionHandler.HandleException(ex);
                _logger.LogError(new EventId((int)rpcEx.StatusCode, nameof(rpcEx.StatusCode)), "Invalid user arguments: " + ex.Message);
                throw rpcEx;
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

            _logger.LogInformation(new EventId((int)AuthorisationEvent.RegisterUser, nameof(RegisterUser)), $"User {request}");

            return reply;
        }
    }
}
