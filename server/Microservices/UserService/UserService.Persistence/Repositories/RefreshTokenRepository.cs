using Microsoft.EntityFrameworkCore;
using UserService.Persistence.Interfaces.Repositories;
using UserService.Persistence.Models;

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
    
    public async Task UpdateTokenAsync(Guid? id, string token, int expirationMinutes)
    {
        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Id == id); 

        refreshToken.Token = token;
        refreshToken.CreatedDate = DateTime.UtcNow;
        refreshToken.ExpiryDate = DateTime.UtcNow.AddMinutes(expirationMinutes);
    }
    
    public async Task<RefreshTokenModel?> GetRefreshTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(rt => rt.User) 
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(b => b.Id == id);
        
        _context.RefreshTokens.Remove(refreshToken);
    }
}