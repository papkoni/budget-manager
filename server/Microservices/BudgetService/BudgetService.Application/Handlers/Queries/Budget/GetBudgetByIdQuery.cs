using BudgetService.Domain.Models;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Budget;

public class GetBudgetByIdQuery(Guid id) : IRequest<BudgetModel>
{
    public Guid Id { get; private set; } = id;
}