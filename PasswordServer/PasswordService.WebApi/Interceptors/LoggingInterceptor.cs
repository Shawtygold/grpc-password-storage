using Grpc.Core;
using Grpc.Core.Interceptors;

namespace PasswordService.WebApi.Interceptors
{
    public class LoggingInterceptor : Interceptor
    {
        private readonly ILogger<LoggingInterceptor> _logger;

        public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                var response = await continuation(request, context);

                _logger.LogInformation("{Date} {Operation} {StatusCode}", DateTime.Now, context.Method, context.Status.StatusCode);

                return response;
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.InvalidArgument)
            {
                _logger.LogWarning("{Date} {Operation} Validation '{Request}' failed", DateTime.Now, context.Method, request.GetType().Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Date} {Operation} {ExMessage}", DateTime.Now, context.Method, ex.Message);
                throw;
            }
        }
    }
}
