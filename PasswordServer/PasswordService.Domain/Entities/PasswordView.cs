namespace PasswordService.Domain.Entities
{
    public class PasswordView
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
    }
}
