using System.ComponentModel.DataAnnotations;

namespace UserService.Application.DTO;

public record RegisterUserRequest
    (
        [Required]string Email,
        [Required] string Password,
        [Required] string Name
    );