namespace PasswordService.Domain.Events
{
    public record BaseEvent
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
