

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyEcommerce.Product.Domain.Common;

public class FullAuditedEntity<TKey> : BaseEntity<TKey>, IFullAuditedEntity<TKey>
{
    public DateTime CreationTime { get; set; }
    [BsonRepresentation(BsonType.String)] public Guid? CreatorId { get; set; }
    public DateTime? UpdateTime { get; set; }
    public Guid? UpdateId { get; set; }
    public DateTime? DeletionTime { get; set; }
    public Guid? DeletionId { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
}