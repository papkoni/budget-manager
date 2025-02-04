using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Models;

namespace UserService.Persistence.Repositories;

public class RefreshTokenRepository: IRefreshTokenRepository
{
    private readonly UserServiceDbContext _context;

    public RefreshTokenRepository(UserServiceDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(RefreshTokenModel refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
    }
    
    public async Task<bool> UpdateTokenAsync(Guid? id, string token, int expirationMinutes)
    {
        //rowsAffected has count of update rows
        var rowsAffected = await _context.RefreshTokens
            .Where(rt => rt.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(rt => rt.Token, token)
                .SetProperty(rt => rt.CreatedDate, DateTime.UtcNow)
                .SetProperty(rt => rt.ExpiryDate, DateTime.UtcNow.AddMinutes(expirationMinutes) )

            );
        
        return rowsAffected > 0;
    }

    public async Task<RefreshTokenModel?> GetRefreshTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(rt => rt.User) 
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }
    
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        var rowsAffected= await _context.RefreshTokens
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();
        
        return rowsAffected > 0;
    }
    
    
}