using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using FluentValidation;
using Mapster;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Category.UpdateCategory;

public class UpdateCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    IValidator<CategoryEntity> validator) : IRequestHandler<UpdateCategoryCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoryRepository.GetAsync(request.Id, cancellationToken)
                       ?? throw new NotFoundException($"Category with id {request.Id} doesn't exists");

        request.Dto.Adapt(category);

        var validationResult = validator.Validate(category);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new BadRequestException(string.Join("; ", errors));
        }
        
        unitOfWork.CategoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}