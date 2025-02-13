using Microsoft.EntityFrameworkCore;
using UserService.Persistence.Interfaces.Repositories;
using UserService.Persistence.Models;

namespace UserService.Persistence.Repositories;

public class UserRepository: IUserRepository
{
    private readonly UserServiceDbContext _context;

    public UserRepository(UserServiceDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(UserModel user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user);
    }
    
    public async Task<UserModel?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return  await _context.Users
            .AsNoTracking()
            .Include(u => u.RefreshToken)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<UserModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(u => u.RefreshToken)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
    
    public void Delete(UserModel user)
    {
        _context.Users.Remove(user);
    }
    
    public void Update(UserModel user)
    {
        _context.Users.Update(user);
    }
    
    public async Task<List<UserModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}