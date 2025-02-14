using UserService.Application.DTO;
using UserService.Persistence.Models;
namespace UserService.Application.Interfaces.Services;


public interface IUserService
{
    Task<List<UsersResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<UserResponse?> GetUserByIdAsync(Guid id, string? userIdClaim, string? userRoleClaim, CancellationToken cancellationToken);
}