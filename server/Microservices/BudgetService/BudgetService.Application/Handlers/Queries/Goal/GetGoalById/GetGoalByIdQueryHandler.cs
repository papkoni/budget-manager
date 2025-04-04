using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Goal.GetGoalById;

public class GetGoalByIdQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetGoalByIdQuery, GoalEntity>
{
    public async Task<GoalEntity> Handle(GetGoalByIdQuery request, CancellationToken cancellationToken)
    {
        var goal = await unitOfWork.GoalRepository.GetAsync(request.Id, cancellationToken)
                     ?? throw new NotFoundException($"Goal with id '{request.Id}' not found.");
        
        return goal;
    }
}