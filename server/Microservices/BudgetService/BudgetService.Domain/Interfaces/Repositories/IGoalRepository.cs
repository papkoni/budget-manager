using BudgetService.Domain.Entities;

namespace BudgetService.Domain.Interfaces.Repositories;

public interface IGoalRepository: IBaseRepository<GoalEntity>
{
    Task<List<GoalEntity>> GetByUserIdAsync(
        Guid userId, 
        CancellationToken cancellationToken);
}