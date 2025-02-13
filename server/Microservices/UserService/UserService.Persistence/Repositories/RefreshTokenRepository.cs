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
    
    public async Task AddAsync(RefreshTokenModel refreshToken, CancellationToken cancellationToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
    }
    
    public void Update(RefreshTokenModel refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);
    }
    
    public async Task<RefreshTokenModel?> GetRefreshTokenAsync(string token, CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens
            .AsNoTracking()
            .Include(rt => rt.User) 
            .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
    }
}