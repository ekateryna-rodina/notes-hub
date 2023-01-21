using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.Contracts;
using LightNote.Application.Models;
using LightNote.Dal.Contracts;

namespace LightNote.Application.Services.TokenGenerators
{
    public class Authenticator : IAuthenticator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public Authenticator(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator, IRefreshTokenRepository refreshTokenRepository)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<AuthenticatedResponse> Authenticate(string identityId, Guid userId, string email)
        {
            var accessToken = _accessTokenGenerator.Generate(identityId, userId, email);
            var refreshToken = _refreshTokenGenerator.Generate();
            await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken, userId);
            return new AuthenticatedResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}