using BudgetService.Domain.Interfaces;
using BudgetService.Domain.Interfaces.Repositories;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using BudgetService.Persistence.Repositories;
using BudgetService.Persistence.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetService.Persistence.Extensions;

public static class PersistenceExtension
{
    public static void AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IBudgetRepository, BudgetRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<IBudgetServiceDbContext,BudgetServiceDbContext>(
            options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(BudgetServiceDbContext)));
            }
        );
    }
}