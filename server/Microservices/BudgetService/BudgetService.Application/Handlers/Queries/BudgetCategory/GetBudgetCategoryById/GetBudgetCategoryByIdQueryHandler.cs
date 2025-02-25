using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.BudgetCategory.GetBudgetCategoryById;

public class GetBudgetCategoryByIdQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetBudgetCategoryByIdQuery, BudgetCategoryEntity>
{
    public async Task<BudgetCategoryEntity> Handle(GetBudgetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var budgetCategory = await unitOfWork.BudgetCategoryRepository.GetAsync(request.Id, cancellationToken)
                             ?? throw new NotFoundException($"Category budget with id '{request.Id}' not found.");

        return budgetCategory;
    }
}

