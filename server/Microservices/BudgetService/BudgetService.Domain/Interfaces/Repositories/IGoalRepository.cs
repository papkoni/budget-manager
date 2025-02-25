using BudgetService.Domain.Entities;

namespace BudgetService.Domain.Interfaces.Repositories;

public interface IGoalRepository
{
    Task<List<GoalEntity>> GetByUserIdAsync(
        Guid userId, 
        CancellationToken cancellationToken);
    Task<GoalEntity?> GetAsync(
        Guid id, 
        CancellationToken cancellationToken);
    Task CreateAsync(
        GoalEntity goal,
        CancellationToken cancellationToken);
    void Update(GoalEntity goal);
    void Delete(GoalEntity goal);
}