using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Models;

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

        if (user == null)
        {
            return false;
        }

        _context.Users.Remove(user);

        return true; 
    }
    
    public async Task<bool> UpdateAsync(UserModel user)
    {
        //rowsAffected has count of update rows
        var rowsAffected = await _context.Users
            .Where(u => u.Id == user.Id)
            .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.Email, user.Email)
                    .SetProperty(u => u.Name, user.Name)
                
            );

        return rowsAffected > 0;
    }
    
    
    public async Task<List<UserModel>> GetAllAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }
}