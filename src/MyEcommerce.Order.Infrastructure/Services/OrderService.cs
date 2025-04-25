using AutoMapper;
using MyEcommerce.Cache;
using MyEcommerce.Order.Application.Dtos;
using MyEcommerce.Order.Application.Interfaces.Repositories;
using MyEcommerce.Order.Application.Interfaces.Services;
using MyEcommerce.Order.Domain.Entities;
using MyEcommerce.Order.Persistence.Contexts;
using MyEcommerce.SharedLibrary.ProductService.Dtos;

namespace MyEcommerce.Order.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly OrderDbContext _dbContext;
    private readonly ICacheService _cacheService;

    public OrderService(IOrderRepository orderRepository, IMapper mapper, OrderDbContext dbContext,
        ICacheService cacheService)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _dbContext = dbContext;
        _cacheService = cacheService;
    }

    public async Task CreateOrderWithOrderDetails(CreateOrderDto request)
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var cache = await _cacheService.GetAsync<List<ProductDto>>("ProductList");

                if (cache is null)
                {
                    //GRPC ile product serviceye git.
                    
                }

                // bool allExist = request.OrderDetails.All(x => cache.Any(t => t.Id == x.ProductId.ToString()));
                // if (!allExist)
                // {
                //     throw new Exception("tanımsız ürün girildi.");
                // }

                var mapping = _mapper.Map<Orders>(request);

                _dbContext.Order.Add(mapping);
                _dbContext.OrderDetails.AddRange(mapping.OrderDetails);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync(); // Başarılıysa commit edilir.
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(); // Hata olursa geri alınır.
                throw;
            }
        }
    }

    public async Task<List<OrderResponseDto>> GetAll()
    {
        var orderList = await _orderRepository.GetListAsync();
        return _mapper.Map<List<OrderResponseDto>>(orderList);
    }
}