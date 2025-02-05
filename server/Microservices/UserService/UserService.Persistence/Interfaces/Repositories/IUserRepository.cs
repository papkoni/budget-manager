using UserService.Application.Models;

namespace UserService.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task AddAsync(UserModel user);
    Task<UserModel?> GetByEmailAsync(string email);
    Task<UserModel?> GetByIdAsync(Guid id);
    Task<bool> DeleteAsync(string id);
    Task<bool> UpdateAsync(UserModel user);
    Task<List<UserModel>> GetAllAsync();
    
}