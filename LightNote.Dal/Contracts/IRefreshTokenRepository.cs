namespace LightNote.Dal.Contracts
{
    public interface IRefreshTokenRepository
    {
        Task AddRefreshTokenAsync(string token, Guid userId);
        Task RemoveRefreshTokensByUserIdAsync(Guid userId);
        Task<Guid> GetUserIdByRefreshTokenAsync(string token);
    }
}