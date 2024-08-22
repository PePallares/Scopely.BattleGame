
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Scopely.BattleGame.JWT.Services
{
    public class JWTokenService : IJWTokenService
    {
        private readonly JWTSettings _settings;

        public JWTokenService(IOptions<JWTSettings> settings)
        {
            _settings = settings.Value;
        }

        public Task<string> GetJWToken(string playerId)
        {
            if (_settings.Key is null) 
            {
                return Task.FromResult(String.Empty);
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("playerId", playerId)
            };

            var Sectoken = new JwtSecurityToken(
              _settings.Issuer,
              _settings.Audience,
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Task.FromResult(token);
        }
    }
}
