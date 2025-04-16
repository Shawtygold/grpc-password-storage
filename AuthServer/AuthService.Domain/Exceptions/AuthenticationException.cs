namespace AuthService.Domain.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException() : base("Invalid login or password") { }
    }
}
