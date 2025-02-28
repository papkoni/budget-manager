using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.BudgetCategory.CreateBudgetCategory;

public record CreateBudgetCategoryCommand(
    Guid BudgetId,
    Guid CategoryId,
    decimal Amount,
    decimal Spent) : IRequest<BudgetCategoryEntity>;