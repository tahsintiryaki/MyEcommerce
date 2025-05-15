using AutoMapper;
using MyEcommerce.Cache;
using MyEcommerce.Order.Application.Dtos;
using MyEcommerce.Order.Application.Interfaces.Repositories;
using MyEcommerce.Order.Application.Interfaces.Services;
using MyEcommerce.Order.Domain.Entities;
using MyEcommerce.Order.Persistence.Contexts;
using MyEcommerce.SharedLibrary;
using MyEcommerce.SharedLibrary.ProductService.Dtos;

namespace MyEcommerce.Order.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDetailsRepository _orderDetailsRepository;
    private readonly IMapper _mapper;
    private readonly OrderDbContext _dbContext;
    private readonly ICacheService _cacheService;
    private readonly ProductChecker.ProductCheckerClient _productChecker;

    public OrderService(IOrderRepository orderRepository, IMapper mapper, OrderDbContext dbContext,
        ICacheService cacheService, ProductChecker.ProductCheckerClient productChecker,
        IOrderDetailsRepository orderDetailsRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _dbContext = dbContext;
        _cacheService = cacheService;
        _productChecker = productChecker;
        _orderDetailsRepository = orderDetailsRepository;
    }

    public async Task CreateOrderWithOrderDetails(CreateOrderDto request)
    {
        var cache = await _cacheService.GetAsync<List<ProductDto>>("ProductList");

        if (cache is null)
        {
            var ids = request.OrderDetails.Select(t => t.ProductId.ToString()).ToList();
            var response = await _productChecker.CheckProductsAsync(new ProductsRequest
            {
                ProductIds = { ids }
            });
            if (response.ProductIds.Count > 0)
            {
                throw new Exception($"Şu ürünler geçersiz: {string.Join(", ", response.ProductIds)}");
            }
        }

        var missingProductIds = request.OrderDetails
            .Where(x => !cache.Any(t => t.Id == x.ProductId.ToString()))
            .Select(x => x.ProductId.ToString())
            .ToList();

        if (missingProductIds.Any())
        {
            var missingIdsString = string.Join(", ", missingProductIds);
            throw new Exception($"Tanımsız ürün(ler) girildi. Eksik Ürün Id(ler)i: {missingIdsString}");
        }

        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var mapping = _mapper.Map<Orders>(request);

                await _orderRepository.InsertAsync(mapping);
                // _dbContext.Order.Add(mapping);
                await _orderDetailsRepository.InsertManyAsync(mapping.OrderDetails);
                // _dbContext.OrderDetails.AddRange(mapping.OrderDetails);
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