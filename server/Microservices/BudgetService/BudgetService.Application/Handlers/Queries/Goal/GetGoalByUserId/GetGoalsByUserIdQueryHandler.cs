using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Goal.GetGoalByUserId;

public class GetGoalsByUserIdQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetGoalsByUserIdQuery, List<GoalEntity>>
{
    public async Task<List<GoalEntity>> Handle(GetGoalsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var goal = await unitOfWork.GoalRepository.GetByUserIdAsync(request.UserId, cancellationToken)
                   ?? throw new NotFoundException($"User with id '{request.UserId}' not found.");
        
        return goal;
    }
}