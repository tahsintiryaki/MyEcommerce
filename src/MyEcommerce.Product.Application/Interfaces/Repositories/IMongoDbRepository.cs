namespace MyEcommerce.Product.Application.Interfaces.Repositories;

public interface IMongoDbRepository<TEntity, TKey>
{
    Task DropCollection();
    Task DropCollection(string collectionName);
    Task<long> CountAsync(CancellationToken cancellationToken = default);
    public Task<List<TEntity>> GetAsync();
    public Task<TEntity> GetAsync(TKey id);
    public Task<TEntity> InsertAsync(TEntity entity);
    public Task InsertManyAsync(List<TEntity> entities);
    public Task<TEntity> UpdateAsync(TKey id, TEntity entity);
    public Task UpdateManyAsync(List<TEntity> entities);
    public Task<bool> DeleteAsync(TEntity entity);
}