using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Goal.GetGoalByUserId;

public class GetGoalsByUserIdQuery(Guid userId) : IRequest<List<GoalEntity>>
{
    public Guid UserId { get; private set; } = userId;
}