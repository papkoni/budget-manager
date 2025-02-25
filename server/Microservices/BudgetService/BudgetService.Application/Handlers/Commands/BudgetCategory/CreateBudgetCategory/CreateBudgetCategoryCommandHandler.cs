using BudgetService.Application.Exceptions;
using BudgetService.Domain.DTO;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using BudgetService.Domain.Interfaces.Validators;
using FluentValidation;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.BudgetCategory.CreateBudgetCategory;

public class CreateBudgetCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    IValidator<BudgetCategoryEntity> validator,
    IBudgetCategoryValidationService validationService) : IRequestHandler<CreateBudgetCategoryCommand, BudgetCategoryEntity>
{
    public async Task<BudgetCategoryEntity> Handle(CreateBudgetCategoryCommand request, CancellationToken cancellationToken)
    {
        var budgetCategory = new BudgetCategoryEntity(
            request.BudgetId,
            request.CategoryId,
            request.Amount,
            request.Spent
        );
        
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
                null),
            cancellationToken);
        
        await unitOfWork.BudgetCategoryRepository.CreateAsync(budgetCategory, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return budgetCategory;
    }
}