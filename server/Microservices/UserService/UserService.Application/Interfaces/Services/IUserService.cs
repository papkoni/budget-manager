using UserService.Application.DTO;

namespace UserService.Application.Interfaces.Services;
using UserService.Application.Models;


public interface IUserService
{
    Task<List<UsersResponce>> GetAllAsync();
    Task<UserModel?> GetUserByIdAsync(Guid id);
    
}