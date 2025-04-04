using BudgetService.Application.DTO;
using BudgetService.Application.Exceptions;
using BudgetService.Application.Interfaces.ValidationServices;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using FluentValidation;
using Mapster;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.BudgetCategory.UpdateBudgetCategory;

public class UpdateBudgetCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    IBudgetCategoryValidationService validationService) : IRequestHandler<UpdateBudgetCategoryCommand, Unit>
{
    public async Task<Unit> Handle(UpdateBudgetCategoryCommand request, CancellationToken cancellationToken)
    {
        var budgetCategory = await unitOfWork.BudgetCategoryRepository.GetAsync(request.Id, cancellationToken) 
                             ?? throw new NotFoundException($"Budget сategory with id {request.Id} doesn't exists");

        request.Dto.Adapt(budgetCategory);

        await validationService.ValidateBudgetCategoriesAsync(
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
