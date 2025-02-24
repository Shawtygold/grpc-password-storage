namespace PasswordService.Model.Exceptions
{
    public class PasswordNotFoundException : Exception
    {
        public PasswordNotFoundException(int entityId) : base($"Password with ID {entityId} not found") { }
    }
}
