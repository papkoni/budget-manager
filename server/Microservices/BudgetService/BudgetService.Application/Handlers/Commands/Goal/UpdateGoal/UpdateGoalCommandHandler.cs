using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using FluentValidation;
using Mapster;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Goal.UpdateGoal;

public class UpdateGoalCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateGoalCommand, Unit>
{
    public async Task<Unit> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await unitOfWork.GoalRepository.GetAsync(request.Id, cancellationToken)
                     ?? throw new NotFoundException($"Goal with id {request.Id} doesn't exists");

        request.Dto.Adapt(goal);
        
        unitOfWork.GoalRepository.Update(goal);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}