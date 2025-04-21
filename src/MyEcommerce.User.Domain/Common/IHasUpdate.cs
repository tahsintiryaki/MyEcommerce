
namespace MyEcommerce.User.Domain.Common;

public interface IHasUpdate
{
    public DateTime? UpdateTime { get; set; }
    public Guid? UpdateId { get; set; }
}