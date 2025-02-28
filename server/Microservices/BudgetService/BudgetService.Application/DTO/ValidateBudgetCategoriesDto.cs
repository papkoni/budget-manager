namespace BudgetService.Application.DTO;

public record ValidateBudgetCategoriesDto(
    Guid CategoryId,
    Guid BudgetId,
    decimal Amount,
    Guid? BudgetCategoryId );