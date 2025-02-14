using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using BudgetService.Domain.Models;
using Mapster;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.CreateBudget;

public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, BudgetEntity>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBudgetCommandHandler(
        IBudgetRepository budgetRepository, 
        IUnitOfWork unitOfWork)
    {
        _budgetRepository = budgetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BudgetEntity> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budgetModel = new BudgetModel(
             request.UserId,
             request.Amount,
             request.Currency,
             request.PeriodType,
             request.StartDate,
             request.EndDate,
             request.Status
        );

        var budgetEntity = budgetModel.Adapt<BudgetEntity>();
        
        await _unitOfWork.Repository<BudgetEntity>().CreateAsync(budgetEntity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return budgetEntity;
    }
}
