using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserService.Persistence;

namespace UserService.Application.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateAsyncScope();

        using UserServiceDbContext libraryDbContext =
            scope.ServiceProvider.GetRequiredService<UserServiceDbContext>();

        libraryDbContext.Database.Migrate();
    }
}