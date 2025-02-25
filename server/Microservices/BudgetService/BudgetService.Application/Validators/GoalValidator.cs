using BudgetService.Domain.Entities;
using FluentValidation;

namespace BudgetService.Application.Validators;

public class GoalValidator : AbstractValidator<GoalEntity>
{
    public GoalValidator()
    {
        RuleFor(x => x.TargetAmount)
            .GreaterThan(0).WithMessage("TargetAmount must be greater than 0");

        RuleFor(x => x.CurrentAmount)
            .GreaterThanOrEqualTo(0).WithMessage("CurrentAmount cannot be negative")
            .LessThanOrEqualTo(x => x.TargetAmount)
            .WithMessage("CurrentAmount cannot exceed TargetAmount");

        RuleFor(x => x.Deadline)
            .GreaterThan(DateTime.UtcNow).WithMessage("Deadline must be in the future");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Goal name is required")
            .MaximumLength(100).WithMessage("Goal name cannot exceed 100 characters");
    }
}
