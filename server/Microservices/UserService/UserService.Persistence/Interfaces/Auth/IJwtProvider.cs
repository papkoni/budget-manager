using UserService.Persistence.Models;

namespace UserService.Persistence.Interfaces.Auth;

public interface IJwtProvider
{
    TokensDTO GenerateTokens(UserModel user);
    string GenerateAccessToken(UserModel user);
    string GenerateRefreshToken(Guid userId);
    int GetRefreshTokenExpirationMinutes();
    bool ValidateRefreshToken(string refreshToken);
    Guid GetUserIdFromRefreshToken(string refreshToken);
}