using MyEcommerce.Order.Application.Interfaces.Repositories;
using MyEcommerce.Order.Domain.Entities;

namespace MyEcommerce.Order.Application.Interfaces.Repositories;

public interface IOrderRepository : IGeneralRepository<Orders>
{
}   