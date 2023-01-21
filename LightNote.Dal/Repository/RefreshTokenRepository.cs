using System.Collections.Concurrent;
using LightNote.Dal.Contracts;
using Microsoft.Extensions.Caching.Distributed;

namespace LightNote.Dal.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        // TODO: Change to another storage provider
        ConcurrentDictionary<string, Guid> _refreshTokens = new ConcurrentDictionary<string, Guid>();
        public Task AddRefreshTokenAsync(string token, Guid userId)
        {
            _refreshTokens.TryAdd(token, userId);
            return Task.CompletedTask;
        }

        public Task<Guid> GetUserIdByRefreshTokenAsync(string token)
        {
            Guid userId = Guid.Empty;
            _refreshTokens.TryGetValue(token, out userId);
            return Task.FromResult(userId);
        }

        public Task RemoveRefreshTokensByUserIdAsync(Guid userId)
        {
            foreach (var item in _refreshTokens.Where(kvp => kvp.Value == userId).ToList())
            {
                _refreshTokens.TryRemove(item.Key, out Guid removed);
            }

            return Task.CompletedTask;
        }
    }
}