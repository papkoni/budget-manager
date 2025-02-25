using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Goal.CreateGoal;

public class CreateGoalCommand(
    Guid userId,
    decimal targetAmount,
    decimal currentAmount,
    DateTime deadline,
    string name) : IRequest<GoalEntity>
{
    public Guid UserId { get; private set; } = userId;                
    public decimal TargetAmount { get; private set; } = targetAmount; 
    public decimal CurrentAmount { get; private set; } = currentAmount; 
    public DateTime Deadline { get; private set; } = deadline;     
    public string Name { get; private set; } = name;     
}