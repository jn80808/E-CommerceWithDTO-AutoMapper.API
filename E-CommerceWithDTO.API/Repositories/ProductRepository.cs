using E_Commerce.API.Models.Domain;
using ECommerceSystem;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _context;

        public ProductRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersFromLastMonthAsync()
        {
            var lastMonth = DateTime.Now.AddMonths(-1);
            return await _context.Orders
                .Where(o => o.OrderDate >= lastMonth)
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetTotalSalesPerProductAsync()
        {
            return await _context.OrderItems
                .GroupBy(oi => oi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    ProductName = g.First().Product.Name,
                    TotalSales = g.Sum(oi => oi.Quantity * oi.UnitPrice)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetTop5ProductsBySalesAsync()
        {
            return await _context.OrderItems
                .GroupBy(oi => oi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    ProductName = g.First().Product.Name,
                    TotalSales = g.Sum(oi => oi.Quantity * oi.UnitPrice)
                })
                .OrderByDescending(p => p.TotalSales)
                .Take(5)
                .ToListAsync();
        }
    }
}
