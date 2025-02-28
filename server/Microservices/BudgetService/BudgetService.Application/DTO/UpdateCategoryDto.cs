namespace BudgetService.Application.DTO;

public record UpdateCategoryDto(
    string Name,
    decimal GlobalLimit,
    decimal GlobalSpent);