using E_Commerce.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ReportsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("products/category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        [HttpGet("orders/lastmonth")]
        public async Task<IActionResult> GetOrdersFromLastMonth()
        {
            var orders = await _productRepository.GetOrdersFromLastMonthAsync();
            return Ok(orders);
        }

        [HttpGet("sales/total")]
        public async Task<IActionResult> GetTotalSalesPerProduct()
        {
            var sales = await _productRepository.GetTotalSalesPerProductAsync();
            return Ok(sales);
        }

        [HttpGet("sales/top5")]
        public async Task<IActionResult> GetTop5ProductsBySales()
        {
            var topProducts = await _productRepository.GetTop5ProductsBySalesAsync();
            return Ok(topProducts);
        }
    }
}
