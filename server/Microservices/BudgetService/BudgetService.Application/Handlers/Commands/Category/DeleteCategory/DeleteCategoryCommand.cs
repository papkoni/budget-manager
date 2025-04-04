using MediatR;

namespace BudgetService.Application.Handlers.Commands.Category.DeleteCategory;

public class DeleteCategoryCommand(Guid id) : IRequest<Unit>
{
    public Guid Id { get; private set; } = id;
}