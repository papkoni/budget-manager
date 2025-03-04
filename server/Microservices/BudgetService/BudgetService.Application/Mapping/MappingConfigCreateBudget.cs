using BudgetService.Application.Handlers.Commands.Budget.CreateBudget;
using BudgetService.Application.Handlers.Commands.Budget.UpdateBudget;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Enums;
using Mapster;

namespace BudgetService.Application.Mapping;

public class MappingConfigCreateBudget: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<CreateBudgetCommand, BudgetEntity>
            .NewConfig()
            .Map(dest => dest.PeriodType, src => Enum.Parse<Period>(src.PeriodType, true))
            .Ignore(dest => dest.Id) 
            .Ignore(dest => dest.CreatedAt)
            .Ignore(dest => dest.UpdatedAt)
            .Ignore(dest => dest.BudgetCategories);
    }
}