using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Google.Rpc;
using Grpc.Core;

namespace RpcExceptionHandlersLib
{
    public class RpcExceptionThrower
    {
        public static void Handle(ValidationException ex)
        {
            var errors = ex.Errors;

            throw new Google.Rpc.Status
            {
                Code = (int)Code.InvalidArgument,
                Message = "Bad request",
                Details =
                    {
                        Any.Pack(new BadRequest
                        {
                            FieldViolations = {
                                errors.Select(item => new BadRequest.Types.FieldViolation{ Field = item.PropertyName, Description = item.ErrorMessage})
                            }
                        })
                    }
            }.ToRpcException();
        }

        public static void Handle(Exception ex)
        {
            throw new Google.Rpc.Status
            {
                Code = (int)Code.Internal,
                Message = "Debug Info",
                Details =
                {
                    Any.Pack(ex.ToRpcDebugInfo())
                }
            }.ToRpcException();
        }
    }
}
