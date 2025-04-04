using System.ComponentModel.DataAnnotations;

namespace UserService.Application.DTO;

public record UsersResponse(
    [Required] Guid Id,
    [Required] string Email,
    [Required] string Name
    );