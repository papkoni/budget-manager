using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class GoalRepository
{
    private readonly IBudgetServiceDbContext _context;

    public GoalRepository(IBudgetServiceDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<GoalEntity>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Goals
            .Where(g => g.UserId == userId)
            .ToListAsync();
    }
}