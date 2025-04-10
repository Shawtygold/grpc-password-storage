using Marten;
using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Domain.Entities;

namespace PasswordService.Infrastructure.Repositories
{
    // Write side
    public class EventSourcingRepository : IEventSourcingRepository<PasswordAggregate>
    {
        private readonly IDocumentStore _documentStore;

        public EventSourcingRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public async Task SaveAsync(PasswordAggregate aggregate, CancellationToken cancellationToken = default)
        {
            var events = aggregate.GetUncommitedEvents();
            if(events.Any())
            {
                using var session = _documentStore.LightweightSession();
                session.Events.Append(aggregate.Id, events);
                await session.SaveChangesAsync(cancellationToken);

                aggregate.ClearUncommitedEvents();
            }
        }

        public async Task<PasswordAggregate?> GetByIdAsync(Guid aggregateId, CancellationToken cancellationToken = default)
        {
            using var session = _documentStore.LightweightSession();
            return await session.Events.AggregateStreamAsync<PasswordAggregate>(aggregateId, token: cancellationToken);
        }
    }
}
