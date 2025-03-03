using AnalysisService.Application.Specifications;

namespace AnalysisService.Persistence.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<T?>> GetAsync(Specification<T> specification, CancellationToken cancellationToken = default);
    Task<List<T?>> GetAllAsync(CancellationToken cancellationToken = default);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}