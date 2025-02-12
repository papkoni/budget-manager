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
    public async Task<IEnumerable<BudgetCategoryEntity>> GetByBudgetIdAsync(Guid budgetId)
    {
        return await _context.BudgetCategories
            .Where(bc => bc.BudgetId == budgetId)
            .ToListAsync();
    }
    
    public async Task AddAsync(BudgetCategoryEntity budgetCategory)
    {
        await _context.BudgetCategories.AddAsync(budgetCategory);
    }
    
    /// <summary>
    /// Обновляет связь между бюджетом и категорией
    /// </summary>
    public void Update(BudgetCategoryEntity budgetCategory)
    {
        _context.BudgetCategories.Update(budgetCategory);
    }
    
    /// <summary>
    /// Удаляет связь категории с бюджетом по идентификатору
    /// </summary>
    public void Delete(BudgetCategoryEntity budgetCategory)
    {
        _context.BudgetCategories.Remove(budgetCategory);
    }
}