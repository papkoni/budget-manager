using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces;
using BudgetService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly IBudgetServiceDbContext _context;

    public BudgetRepository(IBudgetServiceDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<BudgetEntity>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Budgets
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    /// <summary>
    /// Получает активные бюджеты, где текущая дата входит в интервал [StartDate; EndDate] (либо EndDate == null).
    /// </summary>
    public async Task<List<BudgetEntity>> GetActiveBudgetsAsync(DateTime currentDate)
    {
        return await _context.Budgets
            .Where(b => b.StartDate <= currentDate && (b.EndDate == null || b.EndDate >= currentDate))
            .ToListAsync();
    }
}