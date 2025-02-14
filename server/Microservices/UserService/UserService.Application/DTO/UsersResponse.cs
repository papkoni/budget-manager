using System.ComponentModel.DataAnnotations;

namespace UserService.Application.DTO;

public record UsersResponse(
    [Required] string Email,
    [Required] string Name
    );