using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces.Repositories;
using BudgetService.Domain.Interfaces.Repositories.UnitOfWork;
using BudgetService.Persistence.Interfaces;
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
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IBudgetRepository, BudgetRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBudgetCategoryRepository, BudgetCategoryRepository>();
        services.AddScoped<IGoalRepository, GoalRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<IBudgetServiceDbContext,BudgetServiceDbContext>(
            options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(BudgetServiceDbContext)));
            }
        );
    }
}