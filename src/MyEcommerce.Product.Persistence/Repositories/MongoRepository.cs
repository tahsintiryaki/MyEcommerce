using MyEcommerce.Product.Application.Interfaces.Repositories;
using MyEcommerce.Product.Domain.Common;
using MongoDB.Driver;

namespace MyEcommerce.Product.Persistence.Repositories;

public class MongoRepository<TEntity, TKey> : IMongoDbRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : class
{
    protected readonly IMongoCollection<TEntity> Collection;

    public MongoRepository(IMongoDatabase database)
    {
        Collection = database.GetCollection<TEntity>($"{typeof(TEntity).Name}s");
        
    }

    public Task DropCollection()
    {
        return Collection.Database.DropCollectionAsync($"{typeof(TEntity).Name}s");
    }

    public Task DropCollection(string collectionName)
    {
        return Collection.Database.DropCollectionAsync(collectionName);
    }

    public Task<List<TEntity>> GetAsync()
    {
        return Collection.Find(e => true).ToListAsync();
    }

    public Task<TEntity> GetAsync(TKey id)
    {
        return Collection.Find<TEntity>(e => e.Id == id).FirstOrDefaultAsync();
    }

    public Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return Collection.CountDocumentsAsync(e => true, cancellationToken: cancellationToken);
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        if (typeof(IHasCreation).IsAssignableFrom(typeof(TEntity)))
        {
            if (((IHasCreation)entity).CreationTime == DateTime.MinValue)
            {
                ((IHasCreation)entity).CreationTime = DateTime.UtcNow;
            }

            ((IHasCreation)entity).CreatorId = Guid.Empty;
        }

        await Collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task InsertManyAsync(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (typeof(IHasCreation).IsAssignableFrom(typeof(TEntity)))
            {
                ((IHasCreation)entity).CreationTime = DateTime.UtcNow;
                ((IHasCreation)entity).CreatorId = Guid.Empty;
            }
        }

        await Collection.InsertManyAsync(entities);
    }

    public async Task<TEntity> UpdateAsync(TKey id, TEntity entity)
    {
        if (typeof(IHasUpdate).IsAssignableFrom(typeof(TEntity)))
        {
            ((IHasUpdate)entity).UpdateTime = DateTime.UtcNow;
            ((IHasUpdate)entity).UpdateId = Guid.Empty;
        }

        await Collection.ReplaceOneAsync(e => e.Id == id, entity);
        return entity;
    }

    public async Task UpdateManyAsync(List<TEntity> entities)
    {
        var updates = new List<WriteModel<TEntity>>();
        var filterBuilder = Builders<TEntity>.Filter;

        foreach (var entity in entities)
        {
            if (typeof(IHasUpdate).IsAssignableFrom(typeof(TEntity)))
            {
                ((IHasUpdate)entity).UpdateTime = DateTime.Now;
                ((IHasUpdate)entity).UpdateId = Guid.Empty;
            }

            var filter = filterBuilder.Where(x => x.Id == entity.Id);
            updates.Add(new ReplaceOneModel<TEntity>(filter, entity));
        }

        await Collection.BulkWriteAsync(updates);
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        if (typeof(ISoftDeletion).IsAssignableFrom(typeof(TEntity)))
        {
            ((ISoftDeletion)entity).DeletionTime = DateTime.Now;
            ((ISoftDeletion)entity).DeletionId = Guid.NewGuid();
            ((ISoftDeletion)entity).IsDeleted = true;
        }

        await Collection.DeleteOneAsync(e => e.Id == entity.Id);
        return true;
    }
}