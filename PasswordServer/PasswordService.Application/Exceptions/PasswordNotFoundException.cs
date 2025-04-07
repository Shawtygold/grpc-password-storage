namespace PasswordService.Application.Exceptions
{
    public class PasswordNotFoundException : Exception
    {
        public PasswordNotFoundException(Guid entityId) : base($"Password with ID '{entityId}' not found") { }
    }
}
