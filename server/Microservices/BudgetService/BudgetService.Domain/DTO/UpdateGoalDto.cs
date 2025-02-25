namespace BudgetService.Domain.DTO;

public record UpdateGoalDto(
    decimal TargetAmount,
    decimal CurrentAmount,
    DateTime Deadline,
    string Name);