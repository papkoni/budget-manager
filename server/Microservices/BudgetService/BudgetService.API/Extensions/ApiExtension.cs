using BudgetService.API.Behaviors;
using BudgetService.Application.Handlers.Commands.Budget.CreateBudget;
using MediatR;


namespace BudgetService.API.Extensions;

public static class ApiExtension
{
    public static void AddApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssembly(typeof(CreateBudgetCommandHandler).Assembly));
        services.AddTransient(
            typeof(IPipelineBehavior<,>), 
            typeof(ValidationBehavior<,>)
        );
    }
}