using BudgetService.Application.DTO;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.UpdateBudget;

public record UpdateBudgetCommand(
    Guid Id,
    UpdateBudgetDto Dto) : IRequest<Unit>;
