using BudgetService.Domain.Entities;

namespace BudgetService.Domain.Interfaces.Repositories;

public interface IBudgetRepository
{
    Task<List<BudgetEntity>> GetByUserIdAsync(
        Guid userId, 
        CancellationToken cancellationToken);

    Task<List<BudgetEntity>> GetActiveUserBudgetsAsync(
        DateTime currentDate, 
        Guid userId,
        CancellationToken cancellationToken);

    Task<BudgetEntity?> GetAsync(
        Guid id, 
        CancellationToken cancellationToken);
    Task CreateAsync(
        BudgetEntity budget,
        CancellationToken cancellationToken);
    void Update(BudgetEntity budget);
    void Delete(BudgetEntity budget);
}