namespace BudgetService.Application.DTO;

public record UpdateBudgetCategoryDto(
    decimal Amount,
    decimal Spent);