using BudgetService.Application.DTO;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.BudgetCategory.UpdateBudgetCategory;

public record UpdateBudgetCategoryCommand(
    Guid Id,
    UpdateBudgetCategoryDto Dto) : IRequest<Unit>;