namespace MyEcommerce.Product.Domain.Common;

public interface IHasCreation
{
    public DateTime CreationTime { get; set; }
    public Guid? CreatorId { get; set; }
}