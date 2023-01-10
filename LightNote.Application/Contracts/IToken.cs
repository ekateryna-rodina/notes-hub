using System;
using System.IdentityModel.Tokens.Jwt;

namespace LightNote.Application.Contracts
{
    public interface IToken
    {
        JwtSecurityToken GenerateClaimsAndJwtToken(string identityId, Guid userId, string email);
        string WriteToken(JwtSecurityToken token);
    }
}


