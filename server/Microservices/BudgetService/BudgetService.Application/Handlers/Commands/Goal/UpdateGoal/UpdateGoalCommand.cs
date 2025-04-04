using BudgetService.Application.DTO;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Goal.UpdateGoal;

public record UpdateGoalCommand(
    Guid Id,
    UpdateGoalDto Dto) : IRequest<Unit>;