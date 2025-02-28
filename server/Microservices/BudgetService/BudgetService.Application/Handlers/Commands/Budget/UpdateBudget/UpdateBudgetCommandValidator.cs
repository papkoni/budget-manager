using FluentValidation;

namespace BudgetService.Application.Handlers.Commands.Budget.UpdateBudget;

public class UpdateBudgetCommandValidator: AbstractValidator<UpdateBudgetCommand>
{
    public UpdateBudgetCommandValidator()
    {
        RuleFor(x => x.Dto.Amount)
            .GreaterThan(0).WithMessage("Budget amount must be greater than 0");

        RuleFor(x => x.Dto.Currency)
            .NotEmpty().WithMessage("Currency is required");

        RuleFor(x => x.Dto.PeriodType)
            .Must(pt => pt == "monthly" || pt == "yearly")
            .WithMessage("PeriodType must be 'monthly' or 'yearly'");

        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");
    }
}