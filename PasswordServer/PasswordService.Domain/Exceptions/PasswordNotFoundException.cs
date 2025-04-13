namespace PasswordService.Domain.Exceptions
{
    public class PasswordNotFoundException : Exception
    {
        public PasswordNotFoundException(Guid entityId) : base($"Password with ID '{entityId}' not found") 
        {
            PasswordId = entityId;
        }
        public Guid PasswordId { get; }
    }
}
