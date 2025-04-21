
namespace MyEcommerce.User.Domain.Common;

public class FullAuditedEntity : BaseEntity, IFullAuditedEntity
{
    public DateTime CreationTime { get; set; }
    public Guid? CreatorId { get; set; }
    public DateTime? UpdateTime { get; set; }
    public Guid? UpdateId { get; set; }
    public DateTime? DeletionTime { get; set; }
    public Guid? DeletionId { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
}