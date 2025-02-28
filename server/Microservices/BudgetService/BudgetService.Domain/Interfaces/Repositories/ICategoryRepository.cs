using BudgetService.Domain.Entities;

namespace BudgetService.Domain.Interfaces.Repositories;

public interface ICategoryRepository: IBaseRepository<CategoryEntity>
{
    Task<List<CategoryEntity>> GetAllAsync(CancellationToken cancellationToken);
}