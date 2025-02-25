using BudgetService.Domain.Entities;
using FluentValidation;

namespace BudgetService.Application.Validators;

public class BudgetCategoryValidator : AbstractValidator<BudgetCategoryEntity>
{
    public BudgetCategoryValidator()
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
