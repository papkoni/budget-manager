using BudgetService.Application.DTO;

namespace BudgetService.Application.Interfaces.ValidationServices;

public interface IBudgetCategoryValidationService
{ 
    Task ValidateBudgetCategoriesAsync(
        ValidateBudgetCategoriesDto dto, 
        CancellationToken cancellationToken);
}