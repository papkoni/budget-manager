using MongoDB.Driver;

namespace AnalysisService.Persistence.Interfaces;

public interface IAnalysisServiceDBContext
{
    IMongoCollection<T> GetCollection<T>(string name);
}