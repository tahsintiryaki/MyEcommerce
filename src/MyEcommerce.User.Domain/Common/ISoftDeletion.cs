
namespace MyEcommerce.User.Domain.Common;

public interface ISoftDeletion
{
    public DateTime? DeletionTime { get; set; }
    public Guid? DeletionId { get; set; }
    public bool IsDeleted { get; set; }
}