using UserService.Application.DTO;
using UserService.Persistence.Models;

namespace UserService.Application.Interfaces.Services;

public interface IAuthService
{
    Task<TokensDTO> RegisterAsync(RegisterUserRequest registerUser);
    Task<TokensDTO> LoginAsync(string email, string password);
    Task<TokensDTO> RefreshTokensAsync(string refreshToken);
}