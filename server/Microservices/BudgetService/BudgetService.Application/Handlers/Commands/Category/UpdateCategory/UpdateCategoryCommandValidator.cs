using FluentValidation;

namespace BudgetService.Application.Handlers.Commands.Category.UpdateCategory;

public class UpdateCategoryCommandValidator: AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MaximumLength(50).WithMessage("Category name cannot exceed 50 characters");

        RuleFor(x => x.Dto.GlobalLimit)
            .GreaterThan(0).WithMessage("GlobalLimit must be greater than 0");

        RuleFor(x => x.Dto.GlobalSpent)
            .GreaterThanOrEqualTo(0).WithMessage("GlobalSpent cannot be negative")
            .LessThanOrEqualTo(x => x.Dto.GlobalLimit)
            .WithMessage("GlobalSpent cannot exceed GlobalLimit");
    }
}
