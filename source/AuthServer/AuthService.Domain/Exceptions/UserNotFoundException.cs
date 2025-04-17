namespace AuthService.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string userLogin) : base($"User with Login '{userLogin}' not found")
        {
            UserLogin = userLogin;
        }

        public string UserLogin { get; }
    }
}
