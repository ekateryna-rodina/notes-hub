using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.Contracts;
using LightNote.Application.Options;
using LightNote.Dal.Contracts;
using Microsoft.Extensions.Options;

namespace LightNote.Application.Services.TokenGenerators
{
    public class RefreshTokenGenerator
    {
        private readonly IOptions<JwtSettings> _jwtOptions;
        private readonly ITokenGenerator _tokenGenerator;

        public RefreshTokenGenerator(IOptions<JwtSettings> jwtOptions, ITokenGenerator tokenGenerator)
        {
            _jwtOptions = jwtOptions;
            _tokenGenerator = tokenGenerator;
        }

        public string Generate()
        {
            var signingKey = _jwtOptions.Value.RefreshSigningKey;
            var issuer = _jwtOptions.Value.Issuer;
            var audience = _jwtOptions.Value.Audiences[0];
            var expirationMinutes = _jwtOptions.Value.RefreshTokenExpiration;
            var refreshToken = _tokenGenerator.Generate(signingKey, issuer, audience, expirationMinutes);
            return refreshToken;
        }
    }
}