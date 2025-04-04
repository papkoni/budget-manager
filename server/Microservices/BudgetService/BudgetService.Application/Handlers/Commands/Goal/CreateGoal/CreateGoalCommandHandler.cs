using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using FluentValidation;
using MapsterMapper;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Goal.CreateGoal;

public class CreateGoalCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateGoalCommand, GoalEntity>
{
    public async Task<GoalEntity> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = mapper.Map<GoalEntity>(request);
        
        await unitOfWork.GoalRepository.CreateAsync(goal, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return goal;
    }
}