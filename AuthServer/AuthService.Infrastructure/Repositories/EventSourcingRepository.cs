using AuthService.Application.Abstractions.Repositories;
using AuthService.Domain.Entities;
using Marten;

namespace AuthService.Infrastructure.Repositories
{
    public class EventSourcingRepository : IEventSourcingRepository<UserAggregate>
    {
        private readonly IDocumentStore _documentStore;

        public EventSourcingRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public async Task SaveAsync(UserAggregate aggregate, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            IReadOnlyList<object> events = aggregate.GetUncommitedEvents();
            if (events.Any())
            {
                using var session = _documentStore.LightweightSession();
                session.Events.Append(aggregate.Id, events);
                await session.SaveChangesAsync(cancellationToken);

                aggregate.ClearUncommitedEvents();
            }
        }

        public async Task<UserAggregate?> GetByIdAsync(Guid aggregateId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var session = _documentStore.LightweightSession();
            return await session.Events.AggregateStreamAsync<UserAggregate>(aggregateId, token: cancellationToken);
        }
    }
}
