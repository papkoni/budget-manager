using UserService.Persistence.Models;

namespace UserService.Persistence.Interfaces.Repositories;

public interface IUserRepository
{
    Task AddAsync(UserModel user, CancellationToken cancellationToken);
    Task<UserModel?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<UserModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<UserModel>> GetAllAsync(CancellationToken cancellationToken);
    void Update(UserModel user);
    void Delete(UserModel user);
}