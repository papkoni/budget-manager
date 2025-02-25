namespace BudgetService.Domain.DTO;

public record UpdateBudgetCategoryDto(
    decimal Amount,
    decimal Spent);