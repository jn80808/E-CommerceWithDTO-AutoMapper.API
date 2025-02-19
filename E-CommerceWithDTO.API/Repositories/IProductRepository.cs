using E_Commerce.API.Models.Domain;

namespace E_Commerce.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<Order>> GetOrdersFromLastMonthAsync();
        Task<IEnumerable<object>> GetTotalSalesPerProductAsync();
        Task<IEnumerable<object>> GetTop5ProductsBySalesAsync();
    }
}
