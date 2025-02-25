using BudgetService.Application.Exceptions;
using BudgetService.Application.Handlers.Commands.Budget.DeleteBudget;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Goal.DeleteGoal;

public class DeleteGoalCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteGoalCommand,Unit>
{
    public async Task<Unit> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await unitOfWork.GoalRepository.GetAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException($"Goal with id {request.Id} doesn't exists");

        unitOfWork.GoalRepository.Delete(goal);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}