using MyEcommerce.Order.Application.Interfaces.Repositories;
using MyEcommerce.Order.Domain.Entities;
using MyEcommerce.Order.Persistence.Contexts;

namespace MyEcommerce.Order.Persistence.Repositories;

public class OrderDetailsRepository : GeneralRepository<OrderDetails>, IOrderDetailsRepository
{
    public OrderDetailsRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }
}