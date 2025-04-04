using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces;
using BudgetService.Domain.Interfaces.Repositories;
using BudgetService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class GoalRepository(
    IBudgetServiceDbContext context): BaseRepository<GoalEntity>(context),IGoalRepository
{
    public async Task<List<GoalEntity>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(g => g.UserId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}