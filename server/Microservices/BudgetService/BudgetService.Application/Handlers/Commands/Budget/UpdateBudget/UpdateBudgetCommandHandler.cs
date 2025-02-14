using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using Mapster;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.UpdateBudget;

public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, BudgetEntity>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBudgetCommandHandler(IBudgetRepository budgetRepository, IUnitOfWork unitOfWork)
    {
        _budgetRepository = budgetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BudgetEntity> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await _unitOfWork.Repository<BudgetEntity>().GetAsync(request.Id, cancellationToken)
                     ?? throw new NotFoundException($"Budget with id {request.Id} doesn't exists");

        request.Adapt(budget);

        _unitOfWork.Repository<BudgetEntity>().Update(budget);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return budget;
    }
}
