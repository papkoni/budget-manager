using MediatR;

namespace BudgetService.Application.Handlers.Commands.BudgetCategory.DeleteBudgetCategory;

public class DeleteBudgetCategoryCommand(Guid id) : IRequest<Unit>
{
    public Guid Id { get; private set; } = id;
}