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
    
    public async Task<CategoryEntity> GetByIdAsync(Guid id)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);
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
    
    public async Task AddAsync(CategoryEntity category)
    {
        await _context.Categories.AddAsync(category); 
    }
    
    public void Update(CategoryEntity category)
    {
        _context.Categories.Update(category);
    }
    
    public void Delete(CategoryEntity category)
    {
        _context.Categories.Remove(category);
    }
    
    
}