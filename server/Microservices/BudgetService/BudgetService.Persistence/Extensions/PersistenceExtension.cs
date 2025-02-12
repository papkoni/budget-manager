using BudgetService.Domain.Interfaces;
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
        services.AddDbContext<IBudgetServiceDbContext,BudgetServiceDbContext>(
            options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(BudgetServiceDbContext)));
            }
        );
    }
}