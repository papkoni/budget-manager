using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using FluentValidation;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Goal.CreateGoal;

public class CreateGoalCommandHandler(
    IUnitOfWork unitOfWork,
    IValidator<GoalEntity> validator): IRequestHandler<CreateGoalCommand, GoalEntity>
{
    public async Task<GoalEntity> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = new GoalEntity(
            request.UserId,
            request.TargetAmount,
            request.CurrentAmount,
            request.Deadline,
            request.Name
        );
        
        var validationResult = validator.Validate(goal);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new BadRequestException(string.Join("; ", errors));
        }
        
        await unitOfWork.GoalRepository.CreateAsync(goal, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return goal;
    }
}