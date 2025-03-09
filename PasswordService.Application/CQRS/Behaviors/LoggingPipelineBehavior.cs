using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PasswordService.Application.CQRS.Behaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest command, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;

            try
            {
                response = await next();
            }
            catch (ValidationException)
            {
                _logger.LogWarning("{Date} Validation failed for {Command}", DateTime.Now, typeof(TRequest).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Date} {Command} Failure \"{Message}\"", DateTime.Now, typeof(TRequest).Name, ex.Message);
                throw;
            }

            return response;
        }
    }
}
