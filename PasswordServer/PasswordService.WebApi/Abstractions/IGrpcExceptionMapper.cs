using Grpc.Core;

namespace PasswordService.WebApi.Abstractions
{
    public interface IGrpcExceptionMapper
    {
        RpcException MapException(Exception ex);
    }
}
