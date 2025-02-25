using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories;
using BudgetService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class CategoryRepository: ICategoryRepository
{
    private readonly IBudgetServiceDbContext _context;

    public CategoryRepository(IBudgetServiceDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<CategoryEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Categories.ToListAsync(cancellationToken);
    }
    
    public async Task<CategoryEntity?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Categories.FindAsync(id, cancellationToken);
    }
    
    public async Task CreateAsync(CategoryEntity category, CancellationToken cancellationToken)
    {
        await _context.Categories.AddAsync(category, cancellationToken);
    }

    public void Update(CategoryEntity category)
    {
        _context.Categories.Attach(category);
        _context.Categories.Entry(category).State = EntityState.Modified;
    }

    public void Delete(CategoryEntity category)
    {
        _context.Categories.Attach(category);
        _context.Categories.Remove(category);
    }
}