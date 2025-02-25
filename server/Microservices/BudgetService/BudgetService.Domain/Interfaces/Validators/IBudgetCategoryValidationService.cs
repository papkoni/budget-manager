using BudgetService.Domain.DTO;

namespace BudgetService.Domain.Interfaces.Validators;

public interface IBudgetCategoryValidationService
{ 
    Task ValidateBudgetCategoriesForCategoryAsync(
        ValidateBudgetCategoriesDto dto, 
        CancellationToken cancellationToken);
}