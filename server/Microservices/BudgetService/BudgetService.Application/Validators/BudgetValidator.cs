using BudgetService.Domain.Entities;
using FluentValidation;

namespace BudgetService.Application.Validators;

public class BudgetValidator : AbstractValidator<BudgetEntity>
{
    public BudgetValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Budget amount must be greater than 0");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required");

        RuleFor(x => x.PeriodType)
            .Must(pt => pt == "monthly" || pt == "yearly")
            .WithMessage("PeriodType must be 'monthly' or 'yearly'");

        RuleFor(x => x.StartDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("StartDate must not be in the past");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("EndDate must be later than StartDate");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");
    }
}
