namespace BudgetService.Domain.Interfaces.Repositories.UnitOfWork;

public interface IUnitOfWork: IDisposable
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
}