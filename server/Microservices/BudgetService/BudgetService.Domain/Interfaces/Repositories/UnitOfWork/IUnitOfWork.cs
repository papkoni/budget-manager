namespace BudgetService.Domain.Interfaces.Repositories.UnitOfWork;

public interface IUnitOfWork: IDisposable
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
    IBudgetRepository BudgetRepository { get; }
    IBudgetCategoryRepository BudgetCategoryRepository { get; }
    IGoalRepository GoalRepository { get; }
    ICategoryRepository CategoryRepository { get; }
}