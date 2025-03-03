using AnalysisService.Domain.Interfaces.Repositories;
using AnalysisService.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace AnalysisService.Persistence.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
                               ?? configuration.GetConnectionString("AnalysisServiceDBContext");

        var databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME")
                           ?? configuration["MongoDb:DatabaseName"];

        if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(databaseName))
            throw new InvalidOperationException("Database configuration is missing.");

        services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));

        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return new AnalysisServiceDBContext(client, databaseName);
        });

        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IRecommendationRepository, RecommendationRepository>();
        services.AddScoped<IAnalysisRepository, AnalysisRepository>();

        return services;
    }
}