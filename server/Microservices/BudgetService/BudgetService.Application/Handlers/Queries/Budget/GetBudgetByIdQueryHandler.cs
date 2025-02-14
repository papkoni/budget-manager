using BudgetService.Application.Exceptions;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using BudgetService.Domain.Models;
using MapsterMapper;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Budget;

public class GetBudgetByIdQueryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetBudgetByIdQuery, BudgetModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<BudgetModel> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        var budget = await _unitOfWork.Repository<BudgetModel>().GetAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException($"Budget with id '{request.Id}' not found.");

        return _mapper.Map<BudgetModel>(budget);
    }
}