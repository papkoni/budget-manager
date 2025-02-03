using UserService.Application.DTO;
using UserService.Application.Models;

namespace UserService.Application.Interfaces.Services;

public interface IAuthService
{
    Task<TokensDTO> RegisterAsync(string name, string password, string email);
    Task<TokensDTO> LoginAsync(string email, string password);
    Task<bool> LogOutAsync(Guid userId);
    Task<TokensDTO> RefreshTokensAsync(string refreshToken);
}