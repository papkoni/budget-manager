using UserService.Application.DTO;
using UserService.Persistence.Models;

namespace UserService.Application.Interfaces.Auth;

public interface IJwtProvider
{
    TokensDTO GenerateTokens(UserModel user);
    string GenerateAccessToken(UserModel user);
    string GenerateRefreshToken(Guid userId);
    int GetRefreshTokenExpirationMinutes();
    Guid GetUserIdFromRefreshToken(string refreshToken);
    bool ValidateRefreshToken(string refreshToken);
}