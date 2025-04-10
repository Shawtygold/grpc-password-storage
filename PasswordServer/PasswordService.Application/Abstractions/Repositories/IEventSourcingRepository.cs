using PasswordService.Domain.Abstractions;

namespace PasswordService.Application.Abstractions.Repositories
{
    // Write side
    public interface IEventSourcingRepository<TAggregate> where TAggregate : IAggregate, new()
    {
        Task SaveAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
        Task<TAggregate?> GetByIdAsync(Guid aggregateId, CancellationToken cancellationToken = default);
    }
}
