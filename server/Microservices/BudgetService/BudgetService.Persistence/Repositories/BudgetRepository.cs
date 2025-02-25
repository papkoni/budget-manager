using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories;
using BudgetService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly IBudgetServiceDbContext _context;

    public BudgetRepository(IBudgetServiceDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<BudgetEntity>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Budgets
            .Where(b => b.UserId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<List<BudgetEntity>> GetActiveUserBudgetsAsync(
        DateTime currentDate,
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _context.Budgets
            .Where(b => b.StartDate <= currentDate && 
                        (b.EndDate == null || b.EndDate >= currentDate) &&
                        b.UserId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<BudgetEntity?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Budgets.FindAsync(id, cancellationToken);
    }
    
    public async Task CreateAsync(BudgetEntity budget, CancellationToken cancellationToken)
    {
        await _context.Budgets.AddAsync(budget, cancellationToken);
    }

    public void Update(BudgetEntity budget)
    {
        _context.Budgets.Attach(budget);
        _context.Budgets.Entry(budget).State = EntityState.Modified;
    }

    public void Delete(BudgetEntity budget)
    {
        _context.Budgets.Attach(budget);
        _context.Budgets.Remove(budget);
    }
}