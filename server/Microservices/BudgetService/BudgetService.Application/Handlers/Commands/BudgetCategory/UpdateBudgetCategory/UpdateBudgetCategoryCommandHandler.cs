using BudgetService.Application.Exceptions;
using BudgetService.Domain.DTO;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using BudgetService.Domain.Interfaces.Validators;
using FluentValidation;
using Mapster;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.BudgetCategory.UpdateBudgetCategory;

public class UpdateBudgetCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    IValidator<BudgetCategoryEntity> validator,
    IBudgetCategoryValidationService validationService) : IRequestHandler<UpdateBudgetCategoryCommand, Unit>
{
    public async Task<Unit> Handle(UpdateBudgetCategoryCommand request, CancellationToken cancellationToken)
    {
        var budgetCategory = await unitOfWork.BudgetCategoryRepository.GetAsync(request.Id, cancellationToken)
                             ?? throw new NotFoundException($"Budget сategory with id {request.Id} doesn't exists");

        request.Dto.Adapt(budgetCategory);

        var validationResult =  validator.Validate(budgetCategory);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new BadRequestException(string.Join("; ", errors));
        }
        
        await validationService.ValidateBudgetCategoriesForCategoryAsync(
            new ValidateBudgetCategoriesDto(budgetCategory.CategoryId, 
                budgetCategory.BudgetId, 
                budgetCategory.Amount, 
                budgetCategory.Id),
            cancellationToken);
        
        unitOfWork.BudgetCategoryRepository.Update(budgetCategory);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
