using FluentValidation.Results;
using Google.Protobuf.WellKnownTypes;
using Google.Rpc;
using Grpc.Core;
using static Google.Rpc.BadRequest.Types;

namespace PasswordService.WebApi.Exceptions
{
    public class PasswordRpcExceptions
    {
        private const string _domain = "password.service";

        internal static RpcException AlreadyExists(int passwordId)
        {
            ErrorInfo errorInfo = new()
            {
                Domain = _domain,
                Reason = "PASSWORD_ALREADY_EXISTS",
                Metadata = { { "password_id", $"{passwordId}" } }
            };

            return new Google.Rpc.Status()
            {
                Code = (int)StatusCode.AlreadyExists,
                Message = "Password already exists",
                Details = { Any.Pack(errorInfo) }
            }.ToRpcException();
        }

        internal static RpcException NotFound(string passwordId)
        {
            ErrorInfo errorInfo = new()
            {
                Domain = _domain,
                Reason = "PASSWORD_NOT_FOUND",
                Metadata = { { "password_id", $"{passwordId}" } }
            };

            return new Google.Rpc.Status()
            {
                Code = (int)StatusCode.NotFound,
                Message = "Password not found",
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

        internal static RpcException InternalError(Exception ex)
        {
            ErrorInfo errorInfo = new()
            {
                Domain = _domain,
                Reason = "INTERNAL_ERROR",
                Metadata = { { "exception_type", $"{ex.GetType().Name}" }, { "message", $"{ex.Message}" } }
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
