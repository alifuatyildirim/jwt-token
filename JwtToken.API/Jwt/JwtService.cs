using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtToken.API.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly JwtTokenConfiguration _jwtTokenConfiguration;
        public JwtService(JwtTokenConfiguration jwtTokenConfiguration)
        {
            _jwtTokenConfiguration = jwtTokenConfiguration;
        }

        public async Task<string> GenerateToken()
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "alifuat"));
            claims.Add(new Claim(ClaimTypes.Email, "alifuat@yildirim.com"));
            claims.Add(new Claim("id", "2082921"));

            var token = new JwtSecurityToken(
                    _jwtTokenConfiguration.Issuer,
                    _jwtTokenConfiguration.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddHours(4),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfiguration.SecretKey)), SecurityAlgorithms.HmacSha256));

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return await Task.FromResult(tokenString);
        }
    }
}
