using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Budget.CreateBudget;

public class CreateBudgetCommand(
    Guid userId,
    decimal amount,
    string currency,
    string periodType,
    DateTime startDate,
    DateTime? endDate,
    string status) : IRequest<BudgetEntity>
{
    public Guid UserId { get; private set; } = userId;
    public decimal Amount { get; private set; } = amount;
    public string Currency { get; private set; } = currency;
    public string PeriodType { get; private set; } = periodType;
    public DateTime StartDate { get; private set; } = startDate;
    public DateTime? EndDate { get; private set; } = endDate;
    public string Status { get; private set; } = status;
}
