
namespace UserService.Persistence.Models;

public record TokensDTO(
    string RefreshToken,
    string AccessToken
    );