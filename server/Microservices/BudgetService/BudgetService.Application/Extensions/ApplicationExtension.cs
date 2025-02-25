using BudgetService.Application.Validators;
using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Validators;
using FluentValidation;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetService.Application.Extensions;

public static class ApplicationExtension
{
    public static void AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IMapper, Mapper>();
        services.AddScoped<IValidator<BudgetCategoryEntity>,BudgetCategoryValidator>(); 
        services.AddScoped<IBudgetCategoryValidationService, BudgetCategoryValidationService>();
    }
}