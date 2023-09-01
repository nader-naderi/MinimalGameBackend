using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MinimalGameDataLibrary;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;

using System.Text;

namespace ServiceLayer.Services
{
    public interface ITokenService
    {
        string CreateToken(UserData user, List<string> roles);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration) => _configuration = configuration;

        public string CreateToken(UserData user, List<string> roles)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username),
            };

            foreach (var role in roles)
                claims.Add(new(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
