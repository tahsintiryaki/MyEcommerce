namespace MyEcommerce.Order.Domain.Common;

public interface IHasActive
{
    public bool IsActive { get; set; }
}