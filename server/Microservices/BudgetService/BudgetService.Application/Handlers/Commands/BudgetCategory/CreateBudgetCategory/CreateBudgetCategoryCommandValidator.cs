using FluentValidation;

namespace BudgetService.Application.Handlers.Commands.BudgetCategory.CreateBudgetCategory;

public class CreateBudgetCategoryCommandValidator: AbstractValidator<CreateBudgetCategoryCommand>
{
    public CreateBudgetCategoryCommandValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.Spent)
            .GreaterThanOrEqualTo(0).WithMessage("Spent cannot be negative")
            .LessThanOrEqualTo(x => x.Amount)
            .WithMessage("Spent cannot exceed allocated Amount");

        RuleFor(x => x.BudgetId)
            .NotEmpty().WithMessage("BudgetId is required");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required");
    }
}