using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LightNote.Application.Contracts;
using LightNote.Application.Options;
using Microsoft.Extensions.Options;

namespace LightNote.Application.Services
{
    public class JwtService : IToken
    {
        private readonly IOptions<JwtSettings> _jwtOptions;
        public JwtService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        private List<Claim>? RegisterClaims(string identityId, Guid userId, string email)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, identityId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim("UserProfileId", userId.ToString())
            };
        }
        public string WriteToken(JwtSecurityToken? token)
        {
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public JwtSecurityToken? GenerateClaimsAndJwtToken(string identityId, Guid userId, string email)
        {
            var signingKey = _jwtOptions.Value.SigningKey;
            var issuer = _jwtOptions.Value.Issuer;
            var audience = _jwtOptions.Value.Audiences[0];
            var claims = RegisterClaims(identityId, userId, email);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(7);
            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );
            return token;
        }
    }
}

