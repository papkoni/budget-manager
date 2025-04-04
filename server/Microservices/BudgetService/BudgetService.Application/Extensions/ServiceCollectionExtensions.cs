using BudgetService.Application.Behaviors;
using BudgetService.Application.Handlers.Commands.Budget.CreateBudget;
using BudgetService.Application.Handlers.Commands.BudgetCategory;
using BudgetService.Application.Interfaces.ValidationServices;
using BudgetService.Application.Mapping;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetService.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IMapper, Mapper>();
        services.AddValidatorsFromAssemblyContaining<CreateBudgetCommandValidator>();
        services.AddScoped<IBudgetCategoryValidationService, BudgetCategoryValidationService>();
        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)
            );
        services.AddAutoMapper(typeof(BudgetProfile).Assembly);
    }
}