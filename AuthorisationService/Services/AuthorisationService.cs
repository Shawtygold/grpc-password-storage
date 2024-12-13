using AuthorisationService.Enums.EventId;
using AuthorisationService.Interfaces.Services;
using AuthorisationService.Model.Entities;
using FluentValidation;
using Grpc.Core;
using RpcExceptionHandlersLib;

namespace AuthorisationService.Services
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

                if(dbUser != null)
                {
                    reply.User = new() { Id = dbUser.Id, Login = dbUser.Login, Password = dbUser.Password};
                    reply.Success = true;
                }
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning("Invalid user arguments: " + ex.Message);
                RpcExceptionThrower.Handle(ex);            
            }
            catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.NotFound)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError((int)StatusCode.Internal, ex, "Internal Error");
                RpcExceptionThrower.Handle(ex);
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
                _logger.LogWarning("Invalid user arguments: " + ex.Message);
                RpcExceptionThrower.Handle(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)StatusCode.Internal, ex, "Internal Error");
                RpcExceptionThrower.Handle(ex);
            }

            _logger.LogInformation(new EventId((int)AuthorisationEvent.RegisterUser, nameof(RegisterUser)), $"User {request}");

            return reply;
        }
    }
}
