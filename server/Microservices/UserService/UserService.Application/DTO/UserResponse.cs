using System.ComponentModel.DataAnnotations;


namespace UserService.Application.DTO;

public record UserResponse(
    [Required] string Email,
    [Required] string Name,
    [Required] string Role    
    );