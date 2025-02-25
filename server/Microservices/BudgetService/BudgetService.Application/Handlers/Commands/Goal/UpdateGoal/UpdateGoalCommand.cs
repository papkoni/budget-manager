using BudgetService.Domain.DTO;
using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Goal.UpdateGoal;

public class UpdateGoalCommand(
    Guid id,
    UpdateGoalDto dto) : IRequest<Unit>
{
    public Guid Id { get; private set; } = id;                
    public UpdateGoalDto Dto { get; private set; } = dto;
}