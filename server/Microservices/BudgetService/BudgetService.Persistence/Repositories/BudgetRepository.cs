using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly IBudgetServiceDbContext _context;

    public BudgetRepository(IBudgetServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает бюджет по его идентификатору.
    /// </summary>
    public async Task<BudgetEntity> GetByIdAsync(Guid id)
    {
        return await _context.Budgets
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    /// <summary>
    /// Получает список бюджетов конкретного пользователя.
    /// </summary>
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

    /// <summary>
    /// Добавляет новый бюджет в контекст.
    /// </summary>
    public async Task AddAsync(BudgetEntity budget)
    {
        await _context.Budgets.AddAsync(budget);
    }

    /// <summary>
    /// Обновляет существующий бюджет.
    /// </summary>
    public void Update(BudgetEntity budget)
    { 
        _context.Budgets.Update(budget);
    }

    /// <summary>
    /// Удаляет бюджет по его идентификатору.
    /// </summary>
    public void Delete(BudgetEntity budgetEntity)
    { 
        _context.Budgets.Remove(budgetEntity);
    }
}