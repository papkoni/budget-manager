using BudgetService.Application.Exceptions;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.BudgetCategory.DeleteBudgetCategory;

public class DeleteBudgetCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBudgetCategoryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteBudgetCategoryCommand request, CancellationToken cancellationToken)
    {
        var budgetCategory = await unitOfWork.BudgetCategoryRepository.GetAsync(request.Id, cancellationToken)
                             ?? throw new NotFoundException($"Budget category with id {request.Id} doesn't exists");

        unitOfWork.BudgetCategoryRepository.Delete(budgetCategory);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}