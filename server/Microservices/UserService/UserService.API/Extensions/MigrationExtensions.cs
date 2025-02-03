using Microsoft.EntityFrameworkCore;
using UserService.Persistence;

namespace UserService.API.Extensions;

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