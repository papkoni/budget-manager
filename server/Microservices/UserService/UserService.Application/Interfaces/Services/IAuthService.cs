using UserService.Persistence.Models;

namespace UserService.Persistence.Interfaces.Services;

public interface IAuthService
{
    Task<TokensDTO> RegisterAsync(string name, string password, string email);
    Task<TokensDTO> LoginAsync(string email, string password);
    Task<TokensDTO> RefreshTokensAsync(string refreshToken);
}