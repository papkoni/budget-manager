using UserService.Persistence.Models;

namespace UserService.Persistence.Interfaces.Repositories;

public interface IUserRepository
{
    Task AddAsync(UserModel user, CancellationToken cancellationToken);
    Task<UserModel?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<UserModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Delete(UserModel user);
    void Update(UserModel user);
    Task<List<UserModel>> GetAllAsync(CancellationToken cancellationToken);
    
}