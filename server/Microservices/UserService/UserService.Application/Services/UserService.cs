using Mapster;
using UserService.Application.DTO;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Interfaces.Services;
using UserService.Application.Models;

namespace UserService.Application.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
   
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<List<UsersResponce>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Adapt<List<UsersResponce>>();    
    }
    
    public async Task<UserModel?> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
}