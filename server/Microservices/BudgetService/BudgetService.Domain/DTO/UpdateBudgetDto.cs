namespace BudgetService.Domain.DTO;

public record UpdateBudgetDto(
    decimal Amount,
    string Currency,
    string PeriodType,
    string Name);