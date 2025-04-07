using AuthService.Domain.Entities;
using AuthService.Domain.Events;
using Marten.Events.Aggregation;

namespace AuthService.Infrastructure.Projections
{
    public class UserProjection : SingleStreamProjection<User>
    {
        public void Apply(UserRegistered @event, User user)
        {
            user.Id = @event.Id;
            user.Login = @event.Login;
            user.Email = @event.Email;
            user.EncryptedPassword = @event.EncryptedPassword;
            user.CreatedAt = @event.CreatedAt;
        }
    }
}
