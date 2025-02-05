using UserService.Persistence.Models;

namespace UserService.Persistence.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshTokenModel refreshToken);
    Task<bool> UpdateTokenAsync(Guid? id, string token, int expirationMinutes);
    Task<RefreshTokenModel?> GetRefreshTokenAsync(string token);
    Task<bool> DeleteAsync(Guid id);
}