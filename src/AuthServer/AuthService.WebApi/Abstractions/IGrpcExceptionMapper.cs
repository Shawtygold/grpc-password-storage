using Grpc.Core;

namespace AuthService.WebApi.Abstractions
{
    public interface IGrpcExceptionMapper
    {
        RpcException MapException(Exception ex);
    }
}
