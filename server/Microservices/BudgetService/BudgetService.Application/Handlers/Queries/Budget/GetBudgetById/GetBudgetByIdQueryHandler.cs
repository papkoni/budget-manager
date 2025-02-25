using BudgetService.Application.Exceptions;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Budget.GetBudgetById;

public class GetBudgetByIdQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetBudgetByIdQuery, BudgetEntity>
{
    public async Task<BudgetEntity> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        var budget = await unitOfWork.BudgetRepository.GetAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException($"Budget with id '{request.Id}' not found.");

        return budget;
    }
}