using AuthService.Application.Abstractions.Providers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Infrastructure.Providers
{
    public class JWTProvider : IJWTProvider
    {
        private readonly IOptions<JWTSettings> _jwtSettings;

        public JWTProvider(IOptions<JWTSettings> jwtSettings)
        {
           _jwtSettings = jwtSettings;
        }

        public string GetJWTToken(Guid userId)
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
                expires: DateTime.UtcNow.AddHours(int.Parse(_jwtSettings.Value.Lifetime)), // Token lifetime
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
