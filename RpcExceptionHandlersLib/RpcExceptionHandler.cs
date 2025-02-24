using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Google.Rpc;
using Grpc.Core;

namespace RpcExceptionHandlersLib
{
    public class RpcExceptionHandler
    {
        //public static RpcException HandleException(ValidationException ex)
        //{
        //    var errors = ex.Errors;

        //    return new Google.Rpc.Status
        //    {
        //        Code = (int)Code.InvalidArgument,
        //        Message = "Bad request",
        //        Details =
        //            {
        //                Any.Pack(new BadRequest
        //                {
        //                    FieldViolations = {
        //                        errors.Select(item => new BadRequest.Types.FieldViolation{ Field = item.PropertyName, Description = item.ErrorMessage})
        //                    }
        //                })
        //            }
        //    }.ToRpcException();
        //}

        //public static RpcException HandleException(Exception ex)
        //{
        //    return new Google.Rpc.Status
        //    {
        //        Code = (int)Code.Internal,
        //        Message = "Internal Error",
        //        Details =
        //        {
        //            Any.Pack(ex.ToRpcDebugInfo())
        //        }
        //    }.ToRpcException();
        //}
    }
}
