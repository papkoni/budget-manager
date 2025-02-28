using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Goal.CreateGoal;

public record CreateGoalCommand(
    Guid UserId,
    decimal TargetAmount,
    decimal CurrentAmount,
    DateTime Deadline,
    string Name) : IRequest<GoalEntity>;