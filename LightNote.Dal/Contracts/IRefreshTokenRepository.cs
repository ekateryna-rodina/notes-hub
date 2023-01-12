namespace LightNote.Dal.Contracts
{
    public interface IRefreshTokenRepository
    {
        Task AddRefreshTokenAsync(string token, Guid userId);
        Task RemoveRefreshTokenAsync(string token);
        Task<Guid> GetUserIdByRefreshTokenAsync(string token);
    }
}