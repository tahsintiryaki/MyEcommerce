using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Product.Application.Dtos;
using MyEcommerce.Product.Application.Interfaces.Services;

namespace MyEcommerce.Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct(CreateProductDto request)
        {
            var response = await _productService.CreateProduct(request);
            return Ok(response);
        }

        [HttpGet("get-products")]
        public async Task<IActionResult> GetProducts()
        {
            var response = await _productService.GetProducts();
            return Ok(response);
        }
    }
}
