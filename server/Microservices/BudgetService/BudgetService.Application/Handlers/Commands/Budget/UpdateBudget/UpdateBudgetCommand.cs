using BudgetService.Domain.DTO;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.UpdateBudget;

public class UpdateBudgetCommand(
    Guid id,
    UpdateBudgetDto dto) : IRequest<Unit>
{
    public Guid Id { get; private set; } = id;
    public UpdateBudgetDto Dto { get; private set; } = dto;
}
