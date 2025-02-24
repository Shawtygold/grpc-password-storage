using FluentValidation.Results;
using Google.Protobuf.WellKnownTypes;
using Google.Rpc;
using Grpc.Core;
using static Google.Rpc.BadRequest.Types;

namespace PasswordService.Model.Exceptions
{
    public class PasswordRpcExceptions
    {
        internal static RpcException AlreadyExists(string domain, int passwordId)
        {
            ErrorInfo errorInfo = new()
            {
                Domain = domain,
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

        internal static RpcException NotFound(string domain, int passwordId)
        {
            ErrorInfo errorInfo = new()
            {
                Domain = domain,
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

        internal static RpcException InternalError(string domain, Exception ex)
        {
            ErrorInfo errorInfo = new()
            {
                Domain = domain,
                Reason = ex.GetType().Name,
                Metadata = { {"message", $"{ex.Message}" } }
            };

            return new Google.Rpc.Status()
            {
                Code = (int)StatusCode.Internal,
                Message = "Internal Server Error",
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
                Message = "Invalid Arguments",
                Details = { Any.Pack(badRequest) }
            }.ToRpcException();
        }
    }
}
