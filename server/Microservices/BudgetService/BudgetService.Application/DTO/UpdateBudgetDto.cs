namespace BudgetService.Application.DTO;

public record UpdateBudgetDto(
    decimal Amount,
    string Currency,
    string PeriodType,
    string Name);