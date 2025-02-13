using Mapster;
using UserService.Application.DTO;
using UserService.Application.Exceptions;
using UserService.Application.Interfaces.Services;
using UserService.Persistence.Interfaces.Repositories;
using UserService.Persistence.Models;

namespace UserService.Application.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<List<UsersResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        
        return users.Adapt<List<UsersResponse>>();    
    }
    
    public async Task<UserResponse?> GetUserByIdAsync(Guid id, string? userIdClaim, string? userRoleClaim, CancellationToken cancellationToken)
    {
        if (userIdClaim == null)
        {
            throw new UnauthorizedException("Unauthorized access");
        }
        
        bool isOwner = userIdClaim == id.ToString();
        bool isAdmin = userRoleClaim == "Admin";
        if (!isOwner && !isAdmin)
        {
            throw new ForbiddenException("Access is forbidden.");
        }
        
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        
        return user.Adapt<UserResponse>();
    }
}