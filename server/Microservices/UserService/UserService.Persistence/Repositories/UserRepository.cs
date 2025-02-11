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
    
    public async Task AddAsync(UserModel user)
    {
        await _context.Users.AddAsync(user);
    }
    
    public async Task<UserModel?> GetByEmailAsync(string email)
    {
        return  await _context.Users
            .AsNoTracking()
            .Include(u => u.RefreshToken)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<UserModel?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task<bool> DeleteAsync(string id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == Guid.Parse(id));
        
        _context.Users.Remove(user);

        return true; 
    }
    
    public async Task<bool> Update(UserModel user)
    {
        _context.Users.Update(user);
        
        return true; 
    }
    
    public async Task<List<UserModel>> GetAllAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }
}