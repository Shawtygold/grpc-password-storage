using PasswordService.Domain.Abstractions;
using PasswordService.Domain.Events;

namespace PasswordService.Domain.Entities
{
    public class Password : IAggregate
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Login { get; set; } = null!;
        public byte[] EncryptedPassword { get; set; } = null!;
        public string IconPath { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Note { get; set; } = null!;
        public bool IsDeleted {  get; set; }

        private List<object> _uncommitedEvents = new();

        public Password()
        {
            
        }

        public void Apply(object @event)
        {
            switch (@event)
            {
                case PasswordCreated createdEvent:
                    {
                        Apply(createdEvent);
                        _uncommitedEvents.Add(createdEvent);
                        break;
                    }
                case PasswordUpdated updatedEvent:
                    {
                        Apply(updatedEvent);
                        _uncommitedEvents.Add(updatedEvent);
                        break;
                    }
                case PasswordDeleted deletedEvent:
                    {
                        if (IsDeleted) break;

                        Apply(deletedEvent);
                        _uncommitedEvents.Add(deletedEvent);
                        break;
                    }
                default: throw new NotImplementedException();
            }
        }

        private void Apply(PasswordCreated @event)
        {
            Id = @event.Id;
            UserId = @event.UserId;
            Title = @event.Title;
            Login = @event.Login;
            EncryptedPassword = @event.EncryptedPassword;
            IconPath = @event.IconPath;
            Note = @event.Note;
            CreatedAt = @event.CreatedAt;
            UpdatedAt = CreatedAt;
            IsDeleted = false;
        }
        private void Apply(PasswordUpdated @event)
        {
            Title = @event.Title;
            Login = @event.Login;
            EncryptedPassword = @event.EncryptedPassword;
            IconPath = @event.IconPath;
            Note = @event.Note;
            UpdatedAt = @event.UpdatedAt;
        }
        private void Apply(PasswordDeleted @event)
        {
            IsDeleted = true;
        }

        public IReadOnlyList<object> GetUncommitedEvents() => _uncommitedEvents.AsReadOnly();
        public void ClearUncommitedEvents() => _uncommitedEvents.Clear();
    }
}
