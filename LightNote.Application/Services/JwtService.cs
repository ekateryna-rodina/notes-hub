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

        public virtual string GenerateJwtToken(string identityId, Guid userId, string email)
        {
            var signingKey = _jwtOptions.Value.SigningKey;
            var issuer = _jwtOptions.Value.Issuer;
            var audience = _jwtOptions.Value.Audiences[0];
            // Set JWT claims
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, identityId),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Email, email),
        new Claim("UserProfileId", userId.ToString())
    };

            // Set JWT security key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));

            // Set JWT signing credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Set JWT expiration
            var expiration = DateTime.UtcNow.AddDays(7);

            // Create JWT token
            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            // Return JWT as string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

