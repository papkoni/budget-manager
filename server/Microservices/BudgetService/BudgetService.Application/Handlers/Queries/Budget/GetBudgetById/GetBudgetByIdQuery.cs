using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Budget.GetBudgetById;

public class GetBudgetByIdQuery(Guid id) : IRequest<BudgetEntity>
{
    public Guid Id { get; private set; } = id;
}