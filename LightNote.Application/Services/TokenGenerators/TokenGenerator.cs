using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LightNote.Application.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace LightNote.Application.Services.TokenGenerators
{
    public class TokenGenerator : ITokenGenerator
    {
        public string Generate(string signingKey, string issuer, string audience, double expirationMinutes, IEnumerable<Claim>? claims = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(expirationMinutes);
            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token); ;
        }
    }
}