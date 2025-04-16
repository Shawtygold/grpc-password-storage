using AuthService.WebApi.Exceptions;
using FluentValidation;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Spectre.Console;

namespace AuthService.WebApi.Interceptors
{
    public class ValidationInterceptor : Interceptor
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationInterceptor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            IValidator<TRequest> validator;
            try
            {
                validator = _serviceProvider.GetRequiredService<IValidator<TRequest>>();
            }
            catch (InvalidOperationException ex)
            {
                throw AuthRpcExceptions.InternalError(ex);
            }

            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw AuthRpcExceptions.InvalidArgumets(result.Errors);
            }

            return await continuation(request, context);
        }
    }
}
