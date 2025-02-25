using BudgetService.Domain.Entities;

namespace BudgetService.Domain.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<List<CategoryEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<CategoryEntity?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(CategoryEntity category, CancellationToken cancellationToken);
    void Update(CategoryEntity category);
    void Delete(CategoryEntity category);
}