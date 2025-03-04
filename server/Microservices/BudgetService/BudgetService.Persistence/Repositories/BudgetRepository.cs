using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories;
using BudgetService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class BudgetRepository(
    IBudgetServiceDbContext context): BaseRepository<BudgetEntity>(context), IBudgetRepository
{
    public async Task<List<BudgetEntity>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(b => b.UserId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<List<BudgetEntity>> GetActiveUserBudgetsAsync(
        DateTime currentDate,
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(b => b.StartDate <= currentDate && 
                        (b.EndDate == null || b.EndDate >= currentDate) &&
                        b.UserId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}