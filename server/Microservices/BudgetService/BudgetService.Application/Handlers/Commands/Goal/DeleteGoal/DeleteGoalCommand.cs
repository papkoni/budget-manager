using MediatR;

namespace BudgetService.Application.Handlers.Commands.Goal.DeleteGoal;

public class DeleteGoalCommand(Guid id) : IRequest<Unit>
{
    public Guid Id { get; private set; } = id;
}