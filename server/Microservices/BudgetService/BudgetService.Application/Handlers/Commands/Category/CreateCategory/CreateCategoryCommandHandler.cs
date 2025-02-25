using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using FluentValidation;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Category.CreateCategory;

public class CreateCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    IValidator<CategoryEntity> validator) : IRequestHandler<CreateCategoryCommand, CategoryEntity>
{
    public async Task<CategoryEntity> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CategoryEntity(
            request.Name,
            request.GlobalLimit,
            request.GlobalSpent
        );
        
        var validationResult =  validator.Validate(category);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new BadRequestException(string.Join("; ", errors));
        }
        
        await unitOfWork.CategoryRepository.CreateAsync(category, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category;
    }
}
