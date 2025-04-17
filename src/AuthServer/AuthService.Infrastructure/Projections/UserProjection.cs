using AuthService.Domain.Entities;
using AuthService.Domain.Events;
using Marten.Events.Aggregation;

namespace AuthService.Infrastructure.Projections
{
    public class UserProjection : SingleStreamProjection<UserView>
    {
        public UserView Create(UserRegistered @event)
        {
            return new()
            {
                Id = @event.Id,
                Login = @event.Login,
                Email = @event.Email,
                PasswordHash = @event.PasswordHash,
                CreatedAt = @event.CreatedAt
            };
        }
    }
}
