using BudgetService.Domain.Entities;
using BudgetService.Domain.Enums;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.CreateBudget;

public record CreateBudgetCommand(
    Guid UserId,
    decimal Amount,
    string Currency,
    Period PeriodType,
    DateTime StartDate,
    DateTime EndDate,
    string Name) : IRequest<BudgetEntity>;
