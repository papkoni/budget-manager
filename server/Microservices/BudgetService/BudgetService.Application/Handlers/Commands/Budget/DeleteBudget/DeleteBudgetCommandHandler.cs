using BudgetService.Application.Exceptions;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.DeleteBudget;

public class DeleteBudgetCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBudgetCommand,Unit>
{
    public async Task<Unit> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await unitOfWork.BudgetRepository.GetAsync(request.Id, cancellationToken)
                     ?? throw new NotFoundException($"Budget with id {request.Id} doesn't exists");

        unitOfWork.BudgetRepository.Delete(budget);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}