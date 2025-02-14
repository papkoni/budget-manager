using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class CategoryRepository
{
    private readonly IBudgetServiceDbContext _context;

    public CategoryRepository(IBudgetServiceDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<CategoryEntity>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }
    
    /// <summary>
    /// Получает категории по их типу (income/expense)
    /// </summary>
    public async Task<IEnumerable<CategoryEntity>> GetByTypeAsync(string type)
    {
        return await _context.Categories
            .Where(c => c.Type == type)
            .ToListAsync();
    }
}