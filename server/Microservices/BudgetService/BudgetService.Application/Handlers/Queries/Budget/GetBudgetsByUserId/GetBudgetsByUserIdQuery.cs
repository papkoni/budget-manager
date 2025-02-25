using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Budget.GetBudgetsByUserId;

public class GetBudgetsByUserIdQuery(Guid userId) : IRequest<List<BudgetEntity>>
{
    public Guid UserId { get; private set; } = userId;
}