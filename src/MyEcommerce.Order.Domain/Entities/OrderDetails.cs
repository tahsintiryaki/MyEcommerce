using MyEcommerce.Order.Domain.Common;

namespace MyEcommerce.Order.Domain.Entities;

public class OrderDetails : FullAuditedEntity
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }

    public Guid OrdersId { get; set; }
    public Orders Orders { get; set; }
}