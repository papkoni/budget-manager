using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.BudgetCategory.GetBudgetCategoryByBudgetId;

public class GetBudgetCategoryByBudgetIdQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetBudgetCategoryByBudgetIdQuery, List<BudgetCategoryEntity>>
{
    public async Task<List<BudgetCategoryEntity>> Handle(GetBudgetCategoryByBudgetIdQuery request, CancellationToken cancellationToken)
    {
        var budgetCategories = await unitOfWork.BudgetCategoryRepository.
                                   GetByBudgetIdAsync(request.BudgetId, cancellationToken)
                               ?? throw new NotFoundException($"Budget with id '{request.BudgetId}' not found.");

        return budgetCategories;
    }
}