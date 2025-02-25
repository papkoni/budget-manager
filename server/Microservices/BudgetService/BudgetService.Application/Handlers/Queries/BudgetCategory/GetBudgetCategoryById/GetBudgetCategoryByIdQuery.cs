using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.BudgetCategory.GetBudgetCategoryById;

public class GetBudgetCategoryByIdQuery(Guid id) : IRequest<BudgetCategoryEntity>
{
    public Guid Id { get; private set; } = id;
}

