using Marten;
using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Domain.Entities;

namespace PasswordService.Infrastructure.Repositories
{
    // Write side
    public class EventSourcingRepository : IEventSourcingRepository<Password>
    {
        private readonly IDocumentStore _documentStore;

        public EventSourcingRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public async Task SaveAsync(Password aggregate)
        {
            var events = aggregate.GetUncommitedEvents();
            if(events.Any())
            {
                using var session = _documentStore.LightweightSession();
                session.Events.Append(aggregate.Id, events);
                await session.SaveChangesAsync();

                aggregate.ClearUncommitedEvents();
            }
        }

        public async Task<Password?> GetByIdAsync(Guid aggregateId)
        {
            using var session = _documentStore.LightweightSession();
            return await session.Events.AggregateStreamAsync<Password>(aggregateId);
        }
    }
}
