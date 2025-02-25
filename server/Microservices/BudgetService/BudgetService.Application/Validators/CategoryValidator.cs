using BudgetService.Domain.Entities;
using FluentValidation;

namespace BudgetService.Application.Validators;

public class CategoryValidator : AbstractValidator<CategoryEntity>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MaximumLength(50).WithMessage("Category name cannot exceed 50 characters");

        RuleFor(x => x.GlobalLimit)
            .GreaterThan(0).WithMessage("GlobalLimit must be greater than 0");

        RuleFor(x => x.GlobalSpent)
            .GreaterThanOrEqualTo(0).WithMessage("GlobalSpent cannot be negative")
            .LessThanOrEqualTo(x => x.GlobalLimit)
            .WithMessage("GlobalSpent cannot exceed GlobalLimit");
    }
}
