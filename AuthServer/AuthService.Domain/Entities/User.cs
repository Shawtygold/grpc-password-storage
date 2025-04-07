using AuthService.Domain.Events;

namespace AuthService.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[] EncryptedPassword { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public void Apply(UserRegistered @event)
        {
            Login = @event.Login;
            Email = @event.Email;
            EncryptedPassword = @event.EncryptedPassword;
            CreatedAt = @event.CreatedAt;
        }
    }
}
