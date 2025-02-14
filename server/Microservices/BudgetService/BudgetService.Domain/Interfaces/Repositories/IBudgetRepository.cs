using BudgetService.Domain.Entities;

namespace BudgetService.Domain.Interfaces.Repositories;

public interface IBudgetRepository
{
    //унаследовать IGeneric
    Task<List<BudgetEntity>> GetByUserIdAsync(Guid userId);
    Task<List<BudgetEntity>> GetActiveBudgetsAsync(DateTime currentDate);
}