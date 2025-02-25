using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Budget.GetActiveBudgetsByUserId;

public class GetActiveBudgetsByUserIdQuery(Guid userId, DateTime currentDate) : IRequest<List<BudgetEntity>>
{
    public Guid UserId { get; private set; } = userId;
    public DateTime CurrentDate { get; private set; } = currentDate;
}