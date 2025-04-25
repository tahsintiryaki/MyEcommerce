using MyEcommerce.Order.Domain.Common;

namespace MyEcommerce.Order.Application.Interfaces.Repositories;

public interface IGeneralRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetListAsync(bool includeAllDetails = false, CancellationToken cancellationToken = default);
    Task<TEntity> GetAsync(Guid key, bool includeAllDetails = false, CancellationToken cancellationToken = default);

    public Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false,
        CancellationToken cancellationToken = default);

    public Task InsertManyAsync(List<TEntity> entities, bool autoSave = false,
        CancellationToken cancellationToken = default);

    public Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false,
        CancellationToken cancellationToken = default);

    public Task UpdateManyAsync(List<TEntity> entities, bool autoSave = false,
        CancellationToken cancellationToken = default);

    public Task<TEntity> DeleteAsync(TEntity entity, bool autoSave = false,
        CancellationToken cancellationToken = default);

    public Task DeleteManyAsync(List<TEntity> entities, bool autoSave = false,
        CancellationToken cancellationToken = default);

    Task<bool> IsExistAsync(Guid key, CancellationToken cancellationToken = default);
}