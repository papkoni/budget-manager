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

    public async Task<GoalEntity> GetByIdAsync(Guid id)
    {
        return await _context.Goals.FirstOrDefaultAsync(g => g.Id == id);
    }
    
    public async Task<List<GoalEntity>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Goals
            .Where(g => g.UserId == userId)
            .ToListAsync();
    }
    
    public async Task AddAsync(GoalEntity goal)
    {
        await _context.Goals.AddAsync(goal);
    }
    
    public void Update(GoalEntity goal)
    {
        _context.Goals.Update(goal);
    }
    
    public void Delete(GoalEntity goal)
    {
        _context.Goals.Remove(goal);
    }
}