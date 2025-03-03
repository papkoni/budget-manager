using AnalysisService.Application.Specifications;
using AnalysisService.Persistence.Interfaces;
using MongoDB.Driver;

namespace AnalysisService.Persistence.Repositories;

public class BaseRepository<T>(
    IAnalysisServiceDBContext context,  string collectionName): IBaseRepository<T> where T : class
{
    protected readonly IMongoCollection<T> _collection = context.GetCollection<T>(collectionName);
    
    public async Task<List<T?>> GetAsync(Specification<T> specification, CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(specification.ToExpression())
            .ToListAsync(cancellationToken);
    }
    
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<List<T?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(FilterDefinition<T>.Empty).ToListAsync(cancellationToken);
    }
    
    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Guid id, T entity, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", id), entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id), cancellationToken);
    }
}