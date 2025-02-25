using BudgetService.Domain.DTO;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.BudgetCategory.UpdateBudgetCategory;

public class UpdateBudgetCategoryCommand(
    Guid id,
    UpdateBudgetCategoryDto dto) : IRequest<Unit>
{
    public Guid Id { get; private set; } = id;       
    public UpdateBudgetCategoryDto Dto { get; private set; } = dto;       
}