using System;
namespace LightNote.Application.Contracts
{
	public interface IToken
	{
        string GenerateJwtToken(string identityId, Guid userId, string email);
    }
}

