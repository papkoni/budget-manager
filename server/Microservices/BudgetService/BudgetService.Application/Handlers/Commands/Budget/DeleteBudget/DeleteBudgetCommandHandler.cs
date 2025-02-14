using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.DeleteBudget;

public class DeleteBudgetCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBudgetCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await _unitOfWork.Repository<BudgetEntity>().GetAsync(request.Id, cancellationToken)
                     ?? throw new NotFoundException($"Budget with id {request.Id} doesn't exists");

        _unitOfWork.Repository<BudgetEntity>().Delete(budget);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}