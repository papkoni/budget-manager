using FluentValidation;

namespace BudgetService.Application.Handlers.Commands.Goal.UpdateGoal;

public class UpdateGoalCommandValidator: AbstractValidator<UpdateGoalCommand>
{
    public UpdateGoalCommandValidator()
    {
        RuleFor(x => x.Dto.TargetAmount)
            .GreaterThan(0).WithMessage("TargetAmount must be greater than 0");

        RuleFor(x => x.Dto.CurrentAmount)
            .GreaterThanOrEqualTo(0).WithMessage("CurrentAmount cannot be negative")
            .LessThanOrEqualTo(x => x.Dto.TargetAmount)
            .WithMessage("CurrentAmount cannot exceed TargetAmount");

        RuleFor(x => x.Dto.Deadline)
            .GreaterThan(DateTime.UtcNow).WithMessage("Deadline must be in the future");

        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage("Goal name is required")
            .MaximumLength(100).WithMessage("Goal name cannot exceed 100 characters");
    }
}