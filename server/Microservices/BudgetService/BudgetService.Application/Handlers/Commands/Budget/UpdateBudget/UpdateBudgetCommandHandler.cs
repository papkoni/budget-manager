using AutoMapper;
using BudgetService.Application.Exceptions;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;

using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.UpdateBudget;

public class UpdateBudgetCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper): IRequestHandler<UpdateBudgetCommand, Unit>

{
    public async Task<Unit> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await unitOfWork.BudgetRepository.GetAsync(request.Id, cancellationToken)
                     ?? throw new NotFoundException($"Budget with id {request.Id} doesn't exists");

        mapper.Map(request, budget);
        
        unitOfWork.BudgetRepository.Update(budget);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
