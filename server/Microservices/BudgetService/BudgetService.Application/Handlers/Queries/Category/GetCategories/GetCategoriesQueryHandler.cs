using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Category.GetCategories;

public class GetCategoriesQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetCategoriesQuery, List<CategoryEntity>>
{
    public async Task<List<CategoryEntity>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.CategoryRepository.GetAllAsync(cancellationToken);
    }
}