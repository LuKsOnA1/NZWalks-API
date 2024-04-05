using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Repositories.Identity.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Repositories.Identity.Concrete
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _config;

        public TokenRepository(IConfiguration config)
        {
            _config = config;
        }

        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            // Create claims
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create token
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
