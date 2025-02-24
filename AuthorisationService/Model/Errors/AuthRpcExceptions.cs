using AuthService.Model.Entities;
using FluentValidation.Results;
using Google.Protobuf.WellKnownTypes;
using Google.Rpc;
using Grpc.Core;
using static Google.Rpc.BadRequest.Types;

namespace AuthService.Model.Errors
{
    internal class AuthRpcExceptions
    {
        internal static RpcException AlreadyExists(string domain, User user)
        {
            ErrorInfo errorInfo = new()
            {
                Domain = domain,
                Reason = "USER_ALREADY_EXISTS",
                Metadata = { { "user_id", $"{user.Id}" }, { "user_login", $"{user.Login}" }, { "user_password", $"{user.Password}" } }
            };

            return new Google.Rpc.Status()
            {
                Code = (int)StatusCode.AlreadyExists,
                Message = "User already exists",
                Details = { Any.Pack(errorInfo) }
            }.ToRpcException();
        }

        internal static RpcException NotFound(string domain, User user)
        {
            ErrorInfo errorInfo = new()
            {
                Domain = domain,
                Reason = "USER_NOT_FOUND",
                Metadata = { { "user_id", $"{user.Id}" }, { "user_login", $"{user.Login}" }, { "user_password", $"{user.Password}" } }
            };

            return new Google.Rpc.Status()
            {
                Code = (int)StatusCode.NotFound,
                Message = "User not found",
                Details = { Any.Pack(errorInfo) }
            }.ToRpcException();
        }

        internal static RpcException InvalidArgumets(string domain, IEnumerable<ValidationFailure> errors)
        {
            BadRequest badRequest = new()
            {
                FieldViolations = { errors.Select(item => new FieldViolation() { Field = item.PropertyName, Description = item.ErrorMessage }) }
            };

            ErrorInfo errorInfo = new()
            {
                Domain = domain,
                Reason = "INVALID_PASSWORD_ARGUMENTS"
            };

            return new Google.Rpc.Status()
            {
                Code = (int)StatusCode.InvalidArgument,
                Message = "Invalid password arguments",
                Details = { Any.Pack(badRequest), Any.Pack(errorInfo) }
            }.ToRpcException();
        }
    }
}
