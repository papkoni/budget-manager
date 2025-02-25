using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces;
using BudgetService.Domain.Interfaces.Repositories;
using BudgetService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class GoalRepository: IGoalRepository
{
    private readonly IBudgetServiceDbContext _context;

    public GoalRepository(IBudgetServiceDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<GoalEntity>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Goals
            .Where(g => g.UserId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<GoalEntity?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Goals.FindAsync(id, cancellationToken);
    }
    
    public async Task CreateAsync(GoalEntity goal, CancellationToken cancellationToken)
    {
        await _context.Goals.AddAsync(goal, cancellationToken);
    }

    public void Update(GoalEntity goal)
    {
        _context.Goals.Attach(goal);
        _context.Goals.Entry(goal).State = EntityState.Modified;
    }

    public void Delete(GoalEntity goal)
    {
        _context.Goals.Attach(goal);
        _context.Goals.Remove(goal);
    }
}