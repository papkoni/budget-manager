using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class BudgetCategoryRepository
{
    private readonly IBudgetServiceDbContext _context;

    public BudgetCategoryRepository(IBudgetServiceDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Получает все категории, привязанные к конкретному бюджету
    /// </summary>
    public async Task<List<BudgetCategoryEntity>> GetByBudgetIdAsync(Guid budgetId)
    {
        return await _context.BudgetCategories
            .Where(bc => bc.BudgetId == budgetId)
            .ToListAsync();
    }
}