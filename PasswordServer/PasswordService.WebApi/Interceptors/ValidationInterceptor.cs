using FluentValidation;
using Grpc.Core;
using Grpc.Core.Interceptors;
using PasswordService.WebApi.Exceptions;

namespace PasswordService.WebApi.Interceptors
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
                throw PasswordRpcExceptions.InternalError(ex);
            }

            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw PasswordRpcExceptions.InvalidArgumets(result.Errors);
            }

            return await continuation(request, context);
        }
    }
}
