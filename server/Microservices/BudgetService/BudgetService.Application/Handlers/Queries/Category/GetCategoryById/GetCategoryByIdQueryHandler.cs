using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MapsterMapper;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Category.GetCategoryById;

public class GetCategoryByIdQueryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, CategoryEntity>
{
    public async Task<CategoryEntity> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoryRepository.GetAsync(request.Id, cancellationToken)
                     ?? throw new NotFoundException($"Category with id '{request.Id}' not found.");

        return category;
    }
}