using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.Models;

namespace LightNote.Application.Contracts
{
    public interface IAuthenticator
    {
        Task<AuthenticatedResponse> Authenticate(string identityId, Guid userId, string email);
    }
}