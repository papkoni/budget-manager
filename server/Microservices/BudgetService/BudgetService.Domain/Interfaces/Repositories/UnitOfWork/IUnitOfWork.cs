namespace BudgetService.Domain.Interfaces.Repositories.UnitOfWork;

public interface IUnitOfWork: IDisposable
{
    IBudgetRepository BudgetRepository { get; }
    IBudgetCategoryRepository BudgetCategoryRepository { get; }
    IGoalRepository GoalRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    
    Task SaveChangesAsync(CancellationToken cancellationToken);
}