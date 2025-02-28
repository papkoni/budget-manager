using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.CreateBudget;

public class CreateBudgetCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateBudgetCommand, BudgetEntity>
{
    public async Task<BudgetEntity> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = mapper.Map<BudgetEntity>(request);
        
        await unitOfWork.BudgetRepository.CreateAsync(budget, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return budget;
    }
}
