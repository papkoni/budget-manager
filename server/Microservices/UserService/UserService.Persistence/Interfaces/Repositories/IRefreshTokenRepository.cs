using UserService.Persistence.Models;

namespace UserService.Persistence.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshTokenModel refreshToken);
    Task UpdateTokenAsync(Guid? id, string token, int expirationMinutes);
    Task<RefreshTokenModel?> GetRefreshTokenAsync(string token);
    Task DeleteAsync(Guid id);
}