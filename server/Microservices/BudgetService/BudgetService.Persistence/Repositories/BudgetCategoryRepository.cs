using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories;
using BudgetService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class BudgetCategoryRepository(
    IBudgetServiceDbContext context): BaseRepository<BudgetCategoryEntity>(context), IBudgetCategoryRepository
{
    public async Task<List<BudgetCategoryEntity>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(bc => bc.CategoryId == categoryId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<List<BudgetCategoryEntity>> GetBudgetCategoriesByBudgetIdAndExcludingCategoryAsync(Guid budgetId, Guid excludeCategoryId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(bc => bc.BudgetId == budgetId && bc.Id != excludeCategoryId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<List<BudgetCategoryEntity>> GetBudgetCategoriesByBudgetIdAsync(Guid budgetId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(bc => bc.BudgetId == budgetId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<List<BudgetCategoryEntity>> GetByBudgetIdAsync(Guid budgetId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(bc => bc.BudgetId == budgetId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<BudgetCategoryEntity?> GetByBudgetIdAndCategoryIdAsync(Guid budgetId, Guid categoryId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(bc => bc.BudgetId == budgetId && bc.CategoryId == categoryId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }
}