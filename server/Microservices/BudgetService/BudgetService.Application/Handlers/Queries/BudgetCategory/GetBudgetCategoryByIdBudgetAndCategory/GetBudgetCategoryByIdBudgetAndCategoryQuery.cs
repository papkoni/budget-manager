using MediatR;
using BudgetService.Domain.Entities;

namespace BudgetService.Application.Handlers.Queries.BudgetCategory.GetBudgetCategoryByIdBudgetAndCategory;

public record GetBudgetCategoryByIdBudgetAndCategoryQuery(
    Guid BudgetId,
    Guid CategoryId) : IRequest<BudgetCategoryEntity>;