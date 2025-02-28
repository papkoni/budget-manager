using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using FluentValidation;
using Mapster;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.UpdateBudget;

public class UpdateBudgetCommandHandler(
    IUnitOfWork unitOfWork): IRequestHandler<UpdateBudgetCommand, Unit>
{
    public async Task<Unit> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await unitOfWork.BudgetRepository.GetAsync(request.Id, cancellationToken)
                     ?? throw new NotFoundException($"Budget with id {request.Id} doesn't exists");

        request.Dto.Adapt(budget);
        
        unitOfWork.BudgetRepository.Update(budget);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
