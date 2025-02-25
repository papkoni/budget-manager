using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.BudgetCategory.CreateBudgetCategory;

public class CreateBudgetCategoryCommand(
    Guid budgetId,
    Guid categoryId,
    decimal amount,
    decimal spent) : IRequest<BudgetCategoryEntity>
{
    public Guid BudgetId { get; private set; } = budgetId;       
    public Guid CategoryId { get; private set; } = categoryId;   
    public decimal Amount { get; private set; } = amount;       
    public decimal Spent { get; private set; } = spent;         
}