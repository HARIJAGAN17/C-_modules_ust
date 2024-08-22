using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jwt_auth.Authenticate
{
    public class JwtTokenManager : IJwtTokenManager
    {
        IConfiguration _config;
        public JwtTokenManager(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string Authenticate(string username)
        {
            var key = _config.GetValue<string>("jwtAuth:key");
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "http://localhost:5257",
                Audience = "http://localhost:5257",
                Expires = DateTime.UtcNow.AddMinutes(20),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, username),
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),SecurityAlgorithms.HmacSha256),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
