using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Persistence;
using UserService.Persistence.Interfaces;
using UserService.Persistence.Interfaces.Repositories;
using UserService.Persistence.Repositories;

namespace UserService.Application.Extensions;

public static class PersistenceExtension
{
    public static void AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddDbContext<IUserServiceDbContext,UserServiceDbContext>(
            options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(UserServiceDbContext)));
            }
        );
    }
}