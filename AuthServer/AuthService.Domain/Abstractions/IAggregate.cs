namespace AuthService.Domain.Abstractions
{
    public interface IAggregate
    {
        Guid Id { get; set; }
        void Apply(object @event);
        IReadOnlyList<object> GetUncommitedEvents();
        void ClearUncommitedEvents();
    }
}
