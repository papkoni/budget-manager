using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Budget.GetActiveBudgetsByUserId;

public class GetActiveBudgetsByUserIdQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetActiveBudgetsByUserIdQuery, List<BudgetEntity>>
{
    public async Task<List<BudgetEntity>> Handle(GetActiveBudgetsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var budgets = await unitOfWork.BudgetRepository.GetActiveUserBudgetsAsync(request.CurrentDate, request.UserId, cancellationToken)
                     ?? throw new NotFoundException($"User's (with id '{request.UserId}') budgets  not found.");

        return budgets;
    }
}


