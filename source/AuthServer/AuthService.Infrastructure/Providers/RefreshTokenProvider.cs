using AuthService.Application.Abstractions.Providers;
using AuthService.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Infrastructure.Providers
{
    public class RefreshTokenProvider : IRefreshTokenProvider
    {
        private readonly IOptions<JWTSettings> _jwtSettings;
        public RefreshTokenProvider(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings; 
        }

        public string GetAccessToken(Guid userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new ("userId", $"{userId}")
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Value.ValidIssuer,
                audience: _jwtSettings.Value.ValidAudence,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtSettings.Value.LifetimeInMinutes)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
    }
}
