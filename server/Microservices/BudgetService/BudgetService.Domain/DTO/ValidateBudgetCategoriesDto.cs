namespace BudgetService.Domain.DTO;

public record ValidateBudgetCategoriesDto(
    Guid CategoryId,
    Guid BudgetId,
    decimal Amount,
    Guid? BudgetCategoryId );