using BudgetService.Domain.Entities;

namespace BudgetService.Domain.Interfaces;

public interface IBudgetRepository
{
    Task<BudgetEntity> GetByIdAsync(Guid id);
    Task<List<BudgetEntity>> GetByUserIdAsync(Guid userId);
    Task<List<BudgetEntity>> GetActiveBudgetsAsync(DateTime currentDate);
    Task AddAsync(BudgetEntity budget);
    void Update(BudgetEntity budget);
    void Delete(BudgetEntity budgetEntity);
}