using FluentValidation.Results;
using Google.Protobuf.WellKnownTypes;
using Google.Rpc;
using Grpc.Core;
using static Google.Rpc.BadRequest.Types;

namespace AuthService.WebApi.Exceptions
{
    internal class AuthRpcExceptions
    {
        internal static RpcException AlreadyExists(string domain, int userId)
        {
            ErrorInfo errorInfo = new()
            {
                Domain = domain,
                Reason = "USER_ALREADY_EXISTS",
                Metadata = { { "user_id", $"{userId}" } }
            };

            return new Google.Rpc.Status()
            {
                Code = (int)StatusCode.AlreadyExists,
                Message = "User already exists",
                Details = { Any.Pack(errorInfo) }
            }.ToRpcException();
        }

        internal static RpcException NotFound(string domain, string userLogin)
        {
            ErrorInfo errorInfo = new()
            {
                Domain = domain,
                Reason = "USER_NOT_FOUND",
                Metadata = { { "user_login", $"{userLogin}" } }
            };

            return new Google.Rpc.Status()
            {
                Code = (int)StatusCode.NotFound,
                Message = "User not found",
                Details = { Any.Pack(errorInfo) }
            }.ToRpcException();
        }

        internal static RpcException InvalidArgumets(IEnumerable<ValidationFailure> errors)
        {
            BadRequest badRequest = new()
            {
                FieldViolations = { errors.Select(item => new FieldViolation() { Field = item.PropertyName, Description = item.ErrorMessage }) }
            };

            return new Google.Rpc.Status()
            {
                Code = (int)StatusCode.InvalidArgument,
                Message = "Invalid arguments",
                Details = { Any.Pack(badRequest) }
            }.ToRpcException();
        }

        internal static RpcException InternalError(string domain, Exception ex)
        {
            ErrorInfo errorInfo = new()
            {
                Domain = domain,
                Reason = "INTERNAL_ERROR",
                Metadata = {{ "exception_type", $"{ex.GetType().Name}" }, { "message", $"{ex.Message}" } }
            };

            return new Google.Rpc.Status()
            {
                Code = (int)StatusCode.Internal,
                Message = "Internal server error",
                Details = { Any.Pack(errorInfo) }
            }.ToRpcException();
        }
    }
}
