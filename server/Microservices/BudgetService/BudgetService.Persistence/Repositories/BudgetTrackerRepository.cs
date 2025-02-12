using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class BudgetTrackerRepository
{
    private readonly IBudgetServiceDbContext _context;

    public BudgetTrackerRepository(IBudgetServiceDbContext context)
    {
        _context = context;
    }
    
    public async Task<BudgetTrackerEntity> GetByBudgetIdAsync(Guid budgetId)
    {
        return await _context.BudgetTrackers
            .FirstOrDefaultAsync(bt => bt.BudgetId == budgetId);
    }
    
    public async Task AddAsync(BudgetTrackerEntity tracker)
    {
        await _context.BudgetTrackers.AddAsync(tracker);
        // SaveChanges не вызывается здесь!
    }
    
    public void Update(BudgetTrackerEntity tracker)
    {
        _context.BudgetTrackers.Update(tracker);
    }
    
    public void Delete(BudgetTrackerEntity tracker)
    {
        _context.BudgetTrackers.Remove(tracker);
    }
}