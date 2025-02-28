using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.CreateBudget;

public record CreateBudgetCommand(
    Guid UserId,
    decimal Amount,
    string Currency,
    string PeriodType,
    DateTime StartDate,
    DateTime EndDate,
    string Name) : IRequest<BudgetEntity>;
