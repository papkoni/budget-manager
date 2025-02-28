using FluentValidation;

namespace BudgetService.Application.Handlers.Commands.BudgetCategory.UpdateBudgetCategory;

public class UpdateBudgetCategoryCommandValidator: AbstractValidator<UpdateBudgetCategoryCommand>
{
    public UpdateBudgetCategoryCommandValidator()
    {
        RuleFor(x => x.Dto.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.Dto.Spent)
            .GreaterThanOrEqualTo(0).WithMessage("Spent cannot be negative")
            .LessThanOrEqualTo(x => x.Dto.Amount)
            .WithMessage("Spent cannot exceed allocated Amount");
    }
}