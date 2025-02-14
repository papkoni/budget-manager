using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.DeleteBudget;

public class DeleteBudgetCommand(Guid id) : IRequest
{
    public Guid Id { get; private set; } = id;
}