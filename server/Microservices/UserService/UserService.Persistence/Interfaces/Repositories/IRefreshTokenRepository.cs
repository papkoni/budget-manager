using UserService.Persistence.Models;

namespace UserService.Persistence.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshTokenModel refreshToken, CancellationToken cancellationToken);
    void Update(RefreshTokenModel refreshToken);
    Task<RefreshTokenModel?> GetRefreshTokenAsync(string token, CancellationToken cancellationToken); 
}