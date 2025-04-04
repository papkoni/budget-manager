using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.DeleteBudget;

public class DeleteBudgetCommand(Guid id) : IRequest<Unit>
{
    public Guid Id { get; private set; } = id;
}