
namespace UserService.Application.DTO;

public record TokensDTO(
    string RefreshToken,
    string AccessToken
    );