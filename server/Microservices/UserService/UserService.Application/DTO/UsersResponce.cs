using System.ComponentModel.DataAnnotations;

namespace UserService.Application.DTO;

public record UsersResponce(
    [Required] string Email,
    [Required] string Name
    );