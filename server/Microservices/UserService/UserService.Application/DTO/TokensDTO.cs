using UserService.Application.Models;

namespace UserService.Application.DTO;

public record TokensDTO(
    string RefreshToken,
    string AccessToken
    );