using UserService.Application.Models;

namespace UserService.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshTokenModel refreshToken);
    Task<bool> UpdateTokenAsync(Guid? id, string token, int expirationMinutes);
    Task<RefreshTokenModel?> GetRefreshTokenAsync(string token);
    Task<bool> DeleteAsync(Guid id);
}