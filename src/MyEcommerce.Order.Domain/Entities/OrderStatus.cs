using MyEcommerce.Order.Domain.Common;

namespace MyEcommerce.Order.Domain.Entities;

public class OrderStatus : FullAuditedEntity
{
    public string Name { get; set; }
}