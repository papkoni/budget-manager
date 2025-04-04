using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Category.CreateCategory;

public class CreateCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateCategoryCommand, CategoryEntity>
{
    public async Task<CategoryEntity> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<CategoryEntity>(request);
        
        await unitOfWork.CategoryRepository.CreateAsync(category, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category;
    }
}
