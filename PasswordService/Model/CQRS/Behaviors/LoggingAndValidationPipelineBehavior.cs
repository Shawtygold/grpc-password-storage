using FluentValidation;
using MediatR;

namespace PasswordService.Model.CQRS.Behaviors
{
    public class LoggingAndValidationPipelineBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse> 
        where TCommand : class
        where TResponse : class
    {
        private readonly ILogger<LoggingAndValidationPipelineBehavior<TCommand, TResponse>> _logger;
        private readonly IValidator<TCommand> _validator;

        public LoggingAndValidationPipelineBehavior(ILogger<LoggingAndValidationPipelineBehavior<TCommand, TResponse>> logger, IValidator<TCommand> validator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<TResponse> Handle(TCommand command, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(command);

            if (!result.IsValid)
            {
                _logger.LogWarning($"{DateTime.Now} Validation failed for {typeof(TCommand).Name}");
                throw new ValidationException(result.Errors);
            }

            TResponse response;

            try
            {
               response = await next();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} {typeof(TCommand).Name} Failure \"{ex.Message}\"");
                throw;
            }

            _logger.LogInformation($"{DateTime.Now} {typeof(TCommand).Name} Success");

            return response;
        }
    }
}
