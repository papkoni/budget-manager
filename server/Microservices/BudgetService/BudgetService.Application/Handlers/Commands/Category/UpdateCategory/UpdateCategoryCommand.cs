using BudgetService.Application.DTO;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Category.UpdateCategory;

public record UpdateCategoryCommand(
    Guid Id,
    UpdateCategoryDto Dto) : IRequest<Unit>;