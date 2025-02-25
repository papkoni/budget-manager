using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using FluentValidation;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.CreateBudget;

public class CreateBudgetCommandHandler(
    IUnitOfWork unitOfWork,
    IValidator<BudgetEntity> validator) : IRequestHandler<CreateBudgetCommand, BudgetEntity>
{
    public async Task<BudgetEntity> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = new BudgetEntity(
             request.UserId,
             request.Amount,
             request.Currency,
             request.PeriodType,
             request.StartDate,
             request.EndDate,
             request.Name
        );
        
        var validationResult =  validator.Validate(budget);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new BadRequestException(string.Join("; ", errors));
        }
        
        await unitOfWork.BudgetRepository.CreateAsync(budget, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return budget;
    }
}
