using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Goal.GetGoalById;

public class GetGoalByIdQuery(Guid id) : IRequest<GoalEntity>
{
    public Guid Id { get; private set; } = id;
}