using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.BudgetCategory.GetBudgetCategoryByBudgetId;

public class GetBudgetCategoryByBudgetIdQuery(Guid budgetId) : IRequest<List<BudgetCategoryEntity>>
{ 
    public Guid BudgetId { get; private set; } = budgetId;
}