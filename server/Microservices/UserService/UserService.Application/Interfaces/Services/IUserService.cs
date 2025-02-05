using UserService.Application.DTO;
using UserService.Persistence.Models;
namespace UserService.Application.Interfaces.Services;


public interface IUserService
{
    Task<List<UsersResponce>> GetAllAsync();
    Task<UserModel?> GetUserByIdAsync(Guid id);
}