using System.ComponentModel.DataAnnotations;

namespace UserService.Application.DTO;

public record LoginUserRequest
    (
    [Required]string Email,
    [Required] string Password
    );