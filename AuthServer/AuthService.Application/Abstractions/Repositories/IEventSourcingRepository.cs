using AuthService.Domain.Abstractions;

namespace AuthService.Application.Abstractions.Repositories
{
    public interface IEventSourcingRepository<TAggregate> where TAggregate : IAggregate, new()
    {
        Task SaveAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
        Task<TAggregate?> GetByIdAsync(Guid aggregateId, CancellationToken cancellationToken = default);
    }
}
