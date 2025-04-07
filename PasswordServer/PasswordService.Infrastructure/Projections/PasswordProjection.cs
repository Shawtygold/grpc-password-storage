using Marten.Events.Aggregation;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Events;

namespace PasswordService.Infrastructure.Projections
{
    public class PasswordProjection : SingleStreamProjection<PasswordView>
    {
        public PasswordProjection()
        {
            DeleteEvent<PasswordDeleted>();
        }

        public PasswordView Create(PasswordCreated @event)
        {
            return new PasswordView()
            {
                Id = @event.Id,
                UserId = @event.UserId,
                Title = @event.Title,
                Login = @event.Login,
                EncryptedPassword = @event.EncryptedPassword,
                IconPath = @event.IconPath,
                Note = @event.Note,
                CreatedAt = @event.CreatedAt,
                UpdatedAt = @event.CreatedAt
            };
        }

        public void Apply(PasswordUpdated @event, PasswordView passwordView)
        {
            passwordView.Title = @event.Title;
            passwordView.Login = @event.Login;
            passwordView.EncryptedPassword = @event.EncryptedPassword;
            passwordView.IconPath = @event.IconPath;
            passwordView.Note = @event.Note;
            passwordView.UpdatedAt = @event.UpdatedAt;
        }
    }
}
