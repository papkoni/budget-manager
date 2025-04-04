using BudgetService.Application.DTO;
using BudgetService.Application.Interfaces.ValidationServices;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MapsterMapper;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.BudgetCategory.CreateBudgetCategory;

public class CreateBudgetCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    IBudgetCategoryValidationService validationService,
    IMapper mapper) : IRequestHandler<CreateBudgetCategoryCommand, BudgetCategoryEntity>
{
    public async Task<BudgetCategoryEntity> Handle(CreateBudgetCategoryCommand request, CancellationToken cancellationToken)
    {
        var budgetCategory = mapper.Map<BudgetCategoryEntity>(request);
        
        await validationService.ValidateBudgetCategoriesAsync(
            mapper.Map<ValidateBudgetCategoriesDto>(budgetCategory),
            cancellationToken);
        
        await unitOfWork.BudgetCategoryRepository.CreateAsync(budgetCategory, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return budgetCategory;
    }
}