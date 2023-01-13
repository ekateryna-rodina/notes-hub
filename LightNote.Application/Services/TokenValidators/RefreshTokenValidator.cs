using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using LightNote.Application.Contracts;
using LightNote.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LightNote.Application.Services.TokenValidators
{
    public class RefreshTokenValidator : ITokenValidator
    {
        private readonly IOptions<JwtSettings> _jwtConfiguration;
        public RefreshTokenValidator(IOptions<JwtSettings> jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration;
        }
        public bool Validate(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_jwtConfiguration.Value.RefreshSigningKey)),
                ValidateIssuer = true,
                ValidIssuer = _jwtConfiguration.Value.Issuer,
                ValidateAudience = true,
                ValidAudiences = _jwtConfiguration.Value.Audiences,
                ClockSkew = TimeSpan.Zero,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}