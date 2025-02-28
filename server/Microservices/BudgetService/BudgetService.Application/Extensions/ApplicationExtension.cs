using BudgetService.Application.Handlers.Commands.Budget.CreateBudget;
using BudgetService.Application.Handlers.Commands.BudgetCategory;
using BudgetService.Application.Interfaces.ValidationServices;
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
        services.AddValidatorsFromAssemblyContaining<CreateBudgetCommandValidator>();
        services.AddScoped<IBudgetCategoryValidationService, BudgetCategoryValidationService>();
    }
}