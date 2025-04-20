using AuthService.Domain.Abstractions;
using AuthService.Domain.Events;

namespace AuthService.Domain.Entities
{
    public class UserAggregate : IAggregate
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        private readonly List<object> _uncommitedEvents = [];

        public UserAggregate()
        {

        }

        public void Apply(object @event)
        {
            switch (@event)
            {
                case UserRegistered registeredEvent:
                    {
                        Apply(registeredEvent);
                        _uncommitedEvents.Add(registeredEvent);
                        break;
                    }
            }
        }

        private void Apply(UserRegistered @event)
        {
            Id = @event.Id;
            Login = @event.Login;
            Email = @event.Email;
            PasswordHash = @event.PasswordHash;
            CreatedAt = @event.CreatedAt;
        }

        public IReadOnlyList<object> GetUncommitedEvents() => _uncommitedEvents.AsReadOnly();
        public void ClearUncommitedEvents() => _uncommitedEvents.Clear();
    }
}
