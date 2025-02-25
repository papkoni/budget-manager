using System.Collections.Concurrent;
using BudgetService.Domain.Interfaces;
using BudgetService.Domain.Interfaces.Repositories;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;

namespace BudgetService.Persistence.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly BudgetServiceDbContext _context;
    private readonly ConcurrentDictionary<Type, object> _repositories = new();
    
    public UnitOfWork(
        BudgetServiceDbContext context,
        IBudgetRepository budgetRepository,
        IBudgetCategoryRepository budgetCategoryRepository,
        IGoalRepository goalRepository,
        ICategoryRepository categoryRepository
    )
    {
        _context = context;
        BudgetCategoryRepository = budgetCategoryRepository;
        GoalRepository = goalRepository;
        CategoryRepository = categoryRepository;
        BudgetRepository = budgetRepository;
    }
    
    public IBudgetRepository BudgetRepository { get; }
    public IBudgetCategoryRepository BudgetCategoryRepository { get; }
    public IGoalRepository GoalRepository { get; }
    public ICategoryRepository CategoryRepository { get; }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}