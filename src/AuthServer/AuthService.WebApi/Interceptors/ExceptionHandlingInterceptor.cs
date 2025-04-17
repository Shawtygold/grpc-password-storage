using AuthService.WebApi.Abstractions;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace AuthService.WebApi.Interceptors
{
    public class ExceptionHandlingInterceptor : Interceptor
    {
        private readonly IGrpcExceptionMapper _exceptionMapper;

        public ExceptionHandlingInterceptor(IGrpcExceptionMapper grpcExceptionMapper)
        {
            _exceptionMapper = grpcExceptionMapper;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex) when (ex is TaskCanceledException or OperationCanceledException)
            {
                throw new RpcException(new Status(StatusCode.Cancelled, ex.Message));
            }
            catch (Exception ex)
            {
                throw _exceptionMapper.MapException(ex);
            }
        }
    }
}
