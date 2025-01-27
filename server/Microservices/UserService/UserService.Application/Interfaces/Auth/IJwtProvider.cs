using UserService.Application.Models;

namespace UserService.Application.Interfaces.Auth;

public interface IJwtProvider
{
    (string accessToken, string refreshToken) Generate(UserModel user);
    bool ValidateRefreshToken(string refreshToken);
}