using MongoDB.Bson;
using MyEcommerce.Product.Application.Interfaces.Repositories;
using MongoDB.Driver;

namespace MyEcommerce.Product.Persistence.Repositories;

public class ProductRepository : MongoRepository<Domain.Entities.Product, string>, IProductRepository
{
    public ProductRepository(IMongoDatabase database) : base(database)
    {
    }


    public async Task<List<string>> GetNonExistingProductIdsAsync(List<string> ids)
    {
        // Geçerli ObjectId'leri ayıkla ve orijinal ID ile eşle
        var validIds = new Dictionary<string, ObjectId>();

        foreach (var id in ids)
        {
            if (ObjectId.TryParse(id, out var objectId))
                validIds[id] = objectId;
        }

// DB'de olanları bul
        var filter = Builders<Domain.Entities.Product>.Filter.In(p => p.Id, validIds.Values.Select(id => id.ToString()));
        var existingObjectIds = await Collection
            .Find(filter)
            .Project(p => p.Id)
            .ToListAsync();

        var existingIdSet = existingObjectIds.Select(x => x.ToString()).ToHashSet();

// DB'de olmayanları döndür
        var missingIds = validIds
            .Where(kvp => !existingIdSet.Contains(kvp.Value.ToString()))
            .Select(kvp => kvp.Key)
            .ToList();

        return missingIds;
    }
}