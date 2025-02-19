namespace E_Commerce.API.Models.DTO
{
    public class ProductCategoryDto
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
    }

    public class CreateProductCategoryDto
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
    }
}