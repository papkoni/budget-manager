using AutoMapper;
using BudgetService.Application.DTO;
using BudgetService.Application.Handlers.Commands.Budget.CreateBudget;
using BudgetService.Application.Handlers.Commands.Budget.UpdateBudget;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Enums;

namespace BudgetService.Application.Mapping;

public class BudgetProfile : Profile
{
    public BudgetProfile()
    {
        CreateMap<CreateBudgetCommand, BudgetEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.BudgetCategories, opt => opt.Ignore())
            .ForMember(dest => dest.PeriodType, opt => opt.MapFrom(src => src.PeriodType))
            .AfterMap((src, dest) =>
            {
                dest.Id = Guid.NewGuid();
                dest.CreatedAt = DateTime.UtcNow;
                dest.UpdatedAt = DateTime.UtcNow;
            });

        CreateMap<UpdateBudgetDto, BudgetEntity>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.BudgetCategories, opt => opt.Ignore());

        CreateMap<UpdateBudgetCommand, BudgetEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Dto.Amount))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Dto.Currency))
            .ForMember(dest => dest.PeriodType, opt => opt.MapFrom(src => src.Dto.PeriodType))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Dto.Name))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}
