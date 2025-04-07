namespace AuthService.Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string userLogin) : base($"User with Login '{userLogin}' not found") { }
    }
}
