namespace BudgetService.Domain.DTO;

public record UpdateCategoryDto(
    string Name,
    decimal GlobalLimit,
    decimal GlobalSpent);