using AnalysisService.Persistence.Interfaces;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace AnalysisService.Persistence;

public class AnalysisServiceDBContext(
    IMongoClient client, 
    string databaseName): IAnalysisServiceDBContext
{
    private readonly IMongoDatabase _database = client.GetDatabase(databaseName);
    
    static AnalysisServiceDBContext()
    {
        RegisterConventions();
    }
    
    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
    private static void RegisterConventions()
    {
        var conventions = new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new IgnoreIfNullConvention(true),
        };

        ConventionRegistry.Register("DefaultConventions", conventions, _ => true);
    }
}