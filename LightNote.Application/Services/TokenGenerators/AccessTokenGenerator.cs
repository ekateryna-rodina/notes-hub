using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LightNote.Application.Contracts;
using LightNote.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LightNote.Application.Services.TokenGenerators
{
    public class AccessTokenGenerator
    {
        private readonly IOptions<JwtSettings> _jwtOptions;
        private readonly ITokenGenerator _tokenGenerator;

        public AccessTokenGenerator(IOptions<JwtSettings> jwtOptions, ITokenGenerator tokenGenerator)
        {
            _jwtOptions = jwtOptions;
            _tokenGenerator = tokenGenerator;
        }
        private List<Claim>? RegisterClaims(string identityId, Guid userId, string email)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, identityId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim("IdentityId", identityId),
                new Claim("UserProfileId", userId.ToString())
            };
        }

        public string Generate(string identityId, Guid userId, string email)
        {
            var signingKey = _jwtOptions.Value.AccessSigningKey;
            var issuer = _jwtOptions.Value.Issuer;
            var audience = _jwtOptions.Value.Audiences[0];
            var expirationMinutes = _jwtOptions.Value.AccessTokenExpiration;
            var claims = RegisterClaims(identityId, userId, email);
            return _tokenGenerator.Generate(signingKey, issuer, audience, expirationMinutes, claims);
        }
    }
}