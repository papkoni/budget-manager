using System.Collections.Concurrent;
using BudgetService.Domain.Interfaces;
using BudgetService.Domain.Interfaces.Repositories;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;

namespace BudgetService.Persistence.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly BudgetServiceDbContext _context;
    private readonly ConcurrentDictionary<Type, object> _repositories = new();
    
    public IBudgetRepository BudgetRepository { get; }

    public UnitOfWork(
        BudgetServiceDbContext context,
        IBudgetRepository budgetRepository)
    {
        _context = context;
        
        BudgetRepository = budgetRepository;
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            var repositoryInstance = new GenericRepository<TEntity>(_context);
            _repositories[type] = repositoryInstance;
        }

        return (IGenericRepository<TEntity>)_repositories[type];
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}