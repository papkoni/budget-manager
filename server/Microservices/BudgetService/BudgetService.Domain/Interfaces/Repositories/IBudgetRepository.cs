using BudgetService.Domain.Entities;

namespace BudgetService.Domain.Interfaces.Repositories;

public interface IBudgetRepository: IBaseRepository<BudgetEntity>
{
    Task<List<BudgetEntity>> GetByUserIdAsync(
        Guid userId, 
        CancellationToken cancellationToken);

    Task<List<BudgetEntity>> GetActiveUserBudgetsAsync(
        DateTime currentDate, 
        Guid userId,
        CancellationToken cancellationToken);
}