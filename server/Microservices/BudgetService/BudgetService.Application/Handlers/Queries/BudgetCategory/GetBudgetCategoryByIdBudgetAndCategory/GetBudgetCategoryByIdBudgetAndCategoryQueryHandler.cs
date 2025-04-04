using BudgetService.Application.Exceptions;
using BudgetService.Application.Handlers.Queries.BudgetCategory.GetBudgetCategoryByBudgetId;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.BudgetCategory.GetBudgetCategoryByIdBudgetAndCategory;

public class GetBudgetCategoryByIdBudgetAndCategoryQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetBudgetCategoryByIdBudgetAndCategoryQuery, BudgetCategoryEntity>
{
    public async Task<BudgetCategoryEntity> Handle(GetBudgetCategoryByIdBudgetAndCategoryQuery request, CancellationToken cancellationToken)
    {
        var budgetCategories = await unitOfWork.BudgetCategoryRepository.
                                   GetByBudgetIdAndCategoryIdAsync(request.BudgetId,request.CategoryId, cancellationToken)
                               ?? throw new NotFoundException($"Category budget with id '{request.BudgetId}' not found.");

        return budgetCategories;
    }
}