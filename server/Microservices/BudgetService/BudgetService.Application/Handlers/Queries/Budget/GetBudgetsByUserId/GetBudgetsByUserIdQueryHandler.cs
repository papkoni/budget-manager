using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Budget.GetBudgetsByUserId;

public class GetBudgetsByUserIdQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetBudgetsByUserIdQuery, List<BudgetEntity>>
{

    public async Task<List<BudgetEntity>> Handle(GetBudgetsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var budgets = await unitOfWork.BudgetRepository.GetByUserIdAsync(request.UserId, cancellationToken)
                   ?? throw new NotFoundException($"User with id '{request.UserId}' not found.");
        
        return budgets;
    }
}