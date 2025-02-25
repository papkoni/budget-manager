using BudgetService.Domain.DTO;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Category.UpdateCategory;

public class UpdateCategoryCommand(
    Guid id,
    UpdateCategoryDto dto) : IRequest<Unit>
{
    public Guid Id { get; private set; } = id;                     
    public UpdateCategoryDto Dto { get; private set; } = dto;
}
