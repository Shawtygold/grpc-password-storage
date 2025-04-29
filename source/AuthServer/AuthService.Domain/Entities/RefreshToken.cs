namespace AuthService.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = null!;
        public Guid UserId { get; set; }
        public DateTime ExpirationTime { get; set; }

        public RefreshToken(string token, Guid userId, DateTime expirationTime) : this(default, token, userId, expirationTime) { }

        public RefreshToken(Guid id, string token, Guid userId, DateTime expirationTime)
        {
            Id = id;
            Token = token;
            UserId = userId;
            ExpirationTime = expirationTime;
        }
    }
}
