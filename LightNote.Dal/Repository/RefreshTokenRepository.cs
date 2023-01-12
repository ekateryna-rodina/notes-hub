using System.Collections.Concurrent;
using LightNote.Dal.Contracts;

namespace LightNote.Dal.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        Dictionary<string, Guid> _refreshTokens = new Dictionary<string, Guid>();
        public Task AddRefreshTokenAsync(string token, Guid userId)
        {
            _refreshTokens.Add(token, userId);
            return Task.CompletedTask;
        }

        public Task<Guid> GetUserIdByRefreshTokenAsync(string token)
        {
            Guid userId = Guid.Empty;
            _refreshTokens.TryGetValue(token, out userId);
            return Task.FromResult(userId);
        }

        public Task RemoveRefreshTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}