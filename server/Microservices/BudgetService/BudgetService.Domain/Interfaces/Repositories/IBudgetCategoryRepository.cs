using BudgetService.Domain.Entities;

namespace BudgetService.Domain.Interfaces.Repositories;

public interface IBudgetCategoryRepository
{
    Task<List<BudgetCategoryEntity>> GetByCategoryIdAsync(
        Guid categoryId,
        CancellationToken cancellationToken);
    Task<List<BudgetCategoryEntity>> GetBudgetCategoriesByBudgetIdAsync(
        Guid budgetId, 
        CancellationToken cancellationToken);
    Task<List<BudgetCategoryEntity>> GetBudgetCategoriesByBudgetIdAndExcludingCategoryAsync(
        Guid budgetId, 
        Guid excludeCategoryId, 
        CancellationToken cancellationToken);
    Task<List<BudgetCategoryEntity>> GetByBudgetIdAsync(
        Guid budgetId, 
        CancellationToken cancellationToken);
    Task<BudgetCategoryEntity?> GetAsync(
        Guid id, 
        CancellationToken cancellationToken);
    Task CreateAsync(
        BudgetCategoryEntity budgetCategory,
        CancellationToken cancellationToken);
    void Update(BudgetCategoryEntity budgetCategory);
    void Delete(BudgetCategoryEntity budgetCategory);
}