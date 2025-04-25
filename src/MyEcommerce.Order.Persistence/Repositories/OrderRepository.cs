using MyEcommerce.Order.Application.Interfaces.Repositories;
using MyEcommerce.Order.Domain.Entities;
using MyEcommerce.Order.Persistence.Contexts;

namespace MyEcommerce.Order.Persistence.Repositories;

public class OrderRepository : GeneralRepository<Orders>, IOrderRepository
{
    public OrderRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }
}