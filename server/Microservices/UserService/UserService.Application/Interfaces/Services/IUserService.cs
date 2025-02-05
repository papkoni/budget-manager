using UserService.Persistence.Models;
namespace UserService.Persistence.Interfaces.Services;


public interface IUserService
{
    Task<List<UsersResponce>> GetAllAsync();
    Task<UserModel?> GetUserByIdAsync(Guid id);
    
}