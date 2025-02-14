using BudgetService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly BudgetServiceDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(BudgetServiceDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbSet.Attach(entity);
        _dbSet.Remove(entity);
    }
}