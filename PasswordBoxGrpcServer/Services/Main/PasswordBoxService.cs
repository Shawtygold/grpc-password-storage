using FluentValidation;
using Grpc.Core;
using PasswordBoxGrpcServer.Interfaces.Services.Passwords;
using PasswordBoxGrpcServer.Interfaces.Services.Users;
using PasswordBoxGrpcServer.Model.Entities;
using PasswordBoxGrpcServer.Model.Exceptions;

namespace PasswordBoxGrpcServer.Services.Main
{
    public sealed class PasswordBoxService : PasswordBox.PasswordBoxBase
    {
        private readonly ILogger<PasswordBoxService> _logger;
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;

        public PasswordBoxService(ILogger<PasswordBoxService> logger, IUserService userService, IPasswordService passwordService)
        {
            _logger = logger;
            _userService = userService;
            _passwordService = passwordService;
        }

        public override async Task<RegisterUserReply> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            RegisterUserReply reply = new();

            try
            {
                User user = new(request.Login, request.Password);
                await _userService.RegisterAsync(user);
                reply.Success = true;
            }
            catch (ValidationException ex)
            {
                _logger.LogError((int)StatusCode.InvalidArgument, ex, "Invalid user argumets");
                RpcExceptionThrower.ThrowInvalidArgumentException(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)StatusCode.Internal, ex, "Internal Error");
                RpcExceptionThrower.ThrowInternalException(ex);
            }

            return reply;
        }

        public override async Task<AuthenticateUserReply> AuthenticateUser(AuthenticateUserRequest request, ServerCallContext context)
        {
            AuthenticateUserReply reply = new();

            try
            {
                User user = new(request.Login, request.Password);
                reply.Success = await _userService.AuthenticateAsync(user);
            }
            catch (ValidationException ex)
            {
                _logger.LogError((int)StatusCode.InvalidArgument, ex, "Invalid user argumets");
                RpcExceptionThrower.ThrowInvalidArgumentException(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)StatusCode.Internal, ex, "Internal Error");
                RpcExceptionThrower.ThrowInternalException(ex);
            }

            return reply;
        }

        public override async Task<CreatePasswordReply> CreatePassword(CreatePasswordRequest request, ServerCallContext context)
        {
            CreatePasswordReply reply = new();

            try
            {
                Password password = new(request.UserLogin, request.Title, request.Login, request.PasswordValue, request.Commentary, request.Image);
                var replyPassword = await _passwordService.AddPasswordAsync(password);

                reply.Password = new PasswordReply()
                {
                    Id = replyPassword.Id,
                    UserLogin = replyPassword.UserLogin,
                    Title = replyPassword.Title,
                    Login = replyPassword.Login,
                    PasswordValue = replyPassword.PasswordValue,
                    Commentary = replyPassword.Commentary,
                    Image = replyPassword.Image
                };
            }
            catch (ValidationException ex)
            {
                _logger.LogError((int)StatusCode.InvalidArgument, ex, "Invalid password arguments");
                RpcExceptionThrower.ThrowInvalidArgumentException(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)StatusCode.Internal, ex, "Internal Error");
                RpcExceptionThrower.ThrowInternalException(ex);
            }

            return reply;
        }

        public override async Task<UpdatePasswordReply> UpdatePassword(UpdatePasswordRequest request, ServerCallContext context)
        {
            UpdatePasswordReply reply = new();

            try
            {
                Password password = new(request.Id, request.UserLogin, request.Title, request.Login, request.PasswordValue, request.Commentary, request.Image);
                var replyPassword = await _passwordService.UpdatePasswordAsync(password);

                reply.Password = new PasswordReply()
                {
                    Id = replyPassword.Id,
                    UserLogin = replyPassword.UserLogin,
                    Title = replyPassword.Title,
                    Login = replyPassword.Login,
                    PasswordValue = replyPassword.PasswordValue,
                    Commentary = replyPassword.Commentary,
                    Image = replyPassword.Image
                };
            }
            catch (ValidationException ex)
            {
                _logger.LogError((int)StatusCode.InvalidArgument, ex, "Invalid password arguments");
                RpcExceptionThrower.ThrowInvalidArgumentException(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)StatusCode.Internal, ex, "Internal Error");
                RpcExceptionThrower.ThrowInternalException(ex);
            }

            return reply;
        }

        public override async Task<DeletePasswordReply> DeletePassword(DeletePasswordRequest request, ServerCallContext context)
        {
            DeletePasswordReply reply = new();

            try
            {
                await _passwordService.DeletePasswordAsync(request.Id);
                reply.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError((int)StatusCode.Internal, ex, "Internal Error");
                RpcExceptionThrower.ThrowInternalException(ex);
            }

            return reply;
        }

        public override async Task<GetPasswordsReply> GetPasswords(GetPasswordsRequest request, ServerCallContext context)
        {
            List<Password> passwords;
            GetPasswordsReply reply = new();

            try
            {                         
                passwords = new(await _passwordService.GetPasswordsByUserLoginAsync(request.UserLogin));
                var passwordList = passwords.Select(item => new PasswordReply()
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
                RpcExceptionThrower.ThrowInternalException(ex);
            }

            return reply;
        }
    }
}
