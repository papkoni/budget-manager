using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories;
using BudgetService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class BudgetCategoryRepository: IBudgetCategoryRepository
{
    private readonly IBudgetServiceDbContext _context;

    public BudgetCategoryRepository(IBudgetServiceDbContext context)
    {
        _context = context;
    }
 
    public async Task<List<BudgetCategoryEntity>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await _context.BudgetCategories
            .Where(bc => bc.CategoryId == categoryId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<List<BudgetCategoryEntity>> GetBudgetCategoriesByBudgetIdAndExcludingCategoryAsync(Guid budgetId, Guid excludeCategoryId, CancellationToken cancellationToken)
    {
        return await _context.BudgetCategories
            .Where(bc => bc.BudgetId == budgetId && bc.Id != excludeCategoryId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<List<BudgetCategoryEntity>> GetBudgetCategoriesByBudgetIdAsync(Guid budgetId, CancellationToken cancellationToken)
    {
        return await _context.BudgetCategories
            .Where(bc => bc.BudgetId == budgetId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<List<BudgetCategoryEntity>> GetByBudgetIdAsync(Guid budgetId, CancellationToken cancellationToken)
    {
        return await _context.BudgetCategories
            .Where(bc => bc.BudgetId == budgetId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<BudgetCategoryEntity?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.BudgetCategories.FindAsync(id, cancellationToken);
    }
    
    public async Task CreateAsync(BudgetCategoryEntity budgetCategory, CancellationToken cancellationToken)
    {
        await _context.BudgetCategories.AddAsync(budgetCategory, cancellationToken);
    }

    public void Update(BudgetCategoryEntity budgetCategory)
    {
        _context.BudgetCategories.Attach(budgetCategory);
        _context.BudgetCategories.Entry(budgetCategory).State = EntityState.Modified;
    }

    public void Delete(BudgetCategoryEntity budgetCategory)
    {
        _context.BudgetCategories.Attach(budgetCategory);
        _context.BudgetCategories.Remove(budgetCategory);
    }
}