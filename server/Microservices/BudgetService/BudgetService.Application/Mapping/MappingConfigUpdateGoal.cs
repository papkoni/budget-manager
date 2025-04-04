using BudgetService.Application.Handlers.Commands.Budget.UpdateBudget;
using BudgetService.Application.Handlers.Commands.Goal.UpdateGoal;
using BudgetService.Domain.Entities;
using Mapster;

namespace BudgetService.Application.Mapping;

public class MappingConfigUpdateGoal: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateGoalCommand, GoalEntity>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Deadline, src => src.Dto.Deadline)
            .Map(dest => dest.TargetAmount, src => src.Dto.TargetAmount)
            .Map(dest => dest.Name, src => src.Dto.Name)
            .Map(dest => dest.CurrentAmount, src => src.Dto.CurrentAmount);
    }
}