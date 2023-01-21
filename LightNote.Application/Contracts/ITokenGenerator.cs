using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LightNote.Application.Contracts
{
    public interface ITokenGenerator
    {
        string Generate(string signingKey, string issuer, string audience, double expirationMinutes, IEnumerable<Claim>? claims = null);
    }
}