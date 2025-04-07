namespace PasswordService.Domain.Events
{
    public abstract record BaseEvent(Guid StreamId)
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
