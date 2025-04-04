using BudgetService.Application.Handlers.Commands.Category.UpdateCategory;
using BudgetService.Domain.Entities;
using Mapster;

namespace BudgetService.Application.Mapping;

public class MappingConfigUpdateCategory: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateCategoryCommand, CategoryEntity>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.GlobalLimit, src => src.Dto.GlobalLimit)
            .Map(dest => dest.Name, src => src.Dto.Name)
            .Map(dest => dest.GlobalSpent, src => src.Dto.GlobalSpent);
    }
}