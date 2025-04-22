
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyEcommerce.Product.Domain.Common;

public class BaseEntity<TKey> : CoreEntity, IEntity<TKey>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public TKey Id { get; set; }
}