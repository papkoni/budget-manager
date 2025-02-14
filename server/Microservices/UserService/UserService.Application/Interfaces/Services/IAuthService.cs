using UserService.Application.DTO;
using UserService.Persistence.Models;

namespace UserService.Application.Interfaces.Services;

public interface IAuthService
{
    Task<TokensDTO> RegisterAsync(RegisterUserRequest registerUser, CancellationToken cancellationToken);
    Task<TokensDTO> LoginAsync(string email, string password, CancellationToken cancellationToken);
    Task<TokensDTO> RefreshTokensAsync(string refreshToken, CancellationToken cancellationToken);
    Task RevokeTokenAsync(Guid id, CancellationToken cancellationToken);
}