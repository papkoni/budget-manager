using System.Text.Json.Serialization;
using BudgetService.Application.Handlers.Commands.Budget.CreateBudget;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetService.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssembly(typeof(CreateBudgetCommandHandler).Assembly));
    }
}