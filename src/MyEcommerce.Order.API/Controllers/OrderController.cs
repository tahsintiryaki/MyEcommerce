using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Order.Application.Dtos;
using MyEcommerce.Order.Application.Interfaces.Services;
using MyEcommerce.SharedLibrary.IntegrationEvents.Order;

namespace MyEcommerce.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IBus _bus;

        public OrderController(IOrderService orderService, IBus bus)
        {
            _orderService = orderService;
            _bus = bus;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(CreateOrderDto request, CancellationToken cancellationToken)
        {
            await _orderService.CreateOrderWithOrderDetails(request);
            await _bus.Publish(new OrderCreatedEvent { Id = Guid.NewGuid() }, cancellationToken);
            return Ok();
        }

        [HttpGet("get-orders")]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _orderService.GetAll());
        }
    }
}