using BudgetService.Application.Exceptions;
using BudgetService.Domain.DTO;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using BudgetService.Domain.Interfaces.Validators;

namespace BudgetService.Application.Validators;

public class BudgetCategoryValidationService(IUnitOfWork unitOfWork): IBudgetCategoryValidationService
{
    public async Task ValidateBudgetCategoriesForCategoryAsync(ValidateBudgetCategoriesDto dto, CancellationToken cancellationToken)
    {
        decimal totalAmountForBudgetCategories;
        
        var category = await unitOfWork.CategoryRepository.GetAsync(dto.CategoryId, cancellationToken)
                     ?? throw new NotFoundException($"Category with id {dto.BudgetId} doesn't exists");
        
        var budget = await unitOfWork.BudgetRepository.GetAsync(dto.BudgetId, cancellationToken)
                     ?? throw new NotFoundException($"Budget with id {dto.BudgetId} doesn't exists");

        if (dto.BudgetCategoryId.HasValue)
        {
            var existedBudgetCategory = await unitOfWork.BudgetCategoryRepository.GetAsync(dto.BudgetCategoryId.Value, cancellationToken)
                                        ?? throw new NotFoundException($"Category with id {dto.BudgetId} doesn't exists");

            var budgetCategories = await unitOfWork.BudgetCategoryRepository.GetBudgetCategoriesByBudgetIdAndExcludingCategoryAsync(
                dto.BudgetId, existedBudgetCategory.Id, cancellationToken);
            
            totalAmountForBudgetCategories = budgetCategories.Sum(bc => bc.Amount);
        }
        else
        {
            var budgetCategories = await unitOfWork.BudgetCategoryRepository.GetBudgetCategoriesByBudgetIdAsync(dto.BudgetId, cancellationToken);
            totalAmountForBudgetCategories = budgetCategories.Sum(bc => bc.Amount);
        }
        
        var exceedsCategoryLimit = totalAmountForBudgetCategories + dto.Amount > category.GlobalLimit;
        if (exceedsCategoryLimit)
        {
            throw new BadRequestException("Total budget category amount exceeds the category's global limit.");
        }

        var exceedsBudgetLimit = totalAmountForBudgetCategories + dto.Amount > budget.Amount;
        if (exceedsBudgetLimit)
        {
            throw new BadRequestException("Total budget category amount exceeds the budget's available amount.");
        }
    }
}