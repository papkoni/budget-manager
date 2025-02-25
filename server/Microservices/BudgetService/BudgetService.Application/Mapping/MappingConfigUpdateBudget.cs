using BudgetService.Application.Handlers.Commands.Budget.UpdateBudget;
using BudgetService.Domain.Entities;
using Mapster;

namespace BudgetService.Application.Mapping;

public class MappingConfigUpdateBudget: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateBudgetCommand, BudgetEntity>()
            .Map(dest => dest.Id, src => src.Id) 
            .Map(dest => dest.Amount, src => src.Dto.Amount)
            .Map(dest => dest.Currency, src => src.Dto.Currency)
            .Map(dest => dest.PeriodType, src => src.Dto.PeriodType)
            .Map(dest => dest.Name, src => src.Dto.Name)
            .Map(dest => dest.UpdatedAt, _ => DateTime.UtcNow);
    }
}