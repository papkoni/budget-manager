using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetService.Persistence.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateAsyncScope();

        using BudgetServiceDbContext budgetServiceDbContext =
            scope.ServiceProvider.GetRequiredService<BudgetServiceDbContext>();

        budgetServiceDbContext.Database.Migrate();
    }
}