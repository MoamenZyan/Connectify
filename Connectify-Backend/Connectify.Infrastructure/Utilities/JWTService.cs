using Connectify.Application.Interfaces.UtilitesInterfaces;
using Connectify.Infrastructure.Configurations.JWTConfigurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Connectify.Infrastructure.Utilities
{
    public class JWTService : IJWTService
    {
        private readonly JWTConfigurations _jwtConfigurations;
        public JWTService(IOptions<JWTConfigurations> jwtConfigurations)
        {
            _jwtConfigurations = jwtConfigurations.Value;
        }
        public string GenerateToken(Guid userId, string email, string phone)
        {
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwtConfigurations.Issuer,
                Audience = _jwtConfigurations.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigurations.SigningKey)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                   new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userId)!),
                   new Claim(ClaimTypes.MobilePhone, phone),
                   new Claim(ClaimTypes.Email, email),
                }),
                Expires = DateTime.UtcNow.AddMinutes(60)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
