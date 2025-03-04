using BudgetService.Domain.Enums;

namespace BudgetService.Application.DTO;

public record UpdateBudgetDto(
    decimal Amount,
    string Currency,
    Period PeriodType,
    string Name);