using Microsoft.EntityFrameworkCore;
using MyEcommerce.Order.Domain.Entities;

namespace MyEcommerce.Order.Persistence.Contexts;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Orders> Order { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<OrderStatus> OrderStatus { get; set; }
}