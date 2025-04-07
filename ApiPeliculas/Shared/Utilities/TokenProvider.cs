using ApiPeliculas.Entities;
using ApiPeliculas.Shared.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiPeliculas.Shared.Utilities
{
    public class TokenProvider
    {
        private readonly IConfiguration _config;

        public TokenProvider(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken(AppUser user, List<string> roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName), 
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToDouble(_config["Jwt:ExpireHours"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public DateTime GetExpirationTime()
        {
            return DateTime.UtcNow.AddHours(Convert.ToDouble(_config["Jwt:ExpireHours"]));
        }
    }
}
