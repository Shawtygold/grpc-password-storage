namespace AuthService.Domain.Exceptions
{
    public class RefreshTokenExpiredException : Exception
    {
        public RefreshTokenExpiredException() : base("The refresh token has expired") { }
    }
}
