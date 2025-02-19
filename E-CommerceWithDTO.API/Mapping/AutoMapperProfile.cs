using AutoMapper;
using E_Commerce.API.Models.Domain;
using E_Commerce.API.Models.DTO;

namespace E_CommerceWithDTO.API.Mapping
{
    namespace E_Commerce.API.Mappings
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                // Category Mapping
                CreateMap<Category, CategoryDto>().ReverseMap();
                CreateMap<Category, CreateCategoryDto>().ReverseMap();
                CreateMap<Category, UpdateCategoryDto>().ReverseMap();

                // Product Mapping
                CreateMap<Product, ProductDto>().ReverseMap();
                CreateMap<Product, CreateProductDto>().ReverseMap();
                CreateMap<Product, UpdateProductDto>().ReverseMap();

                // Order Mapping
                CreateMap<Order, OrderDto>().ReverseMap();
                CreateMap<Order, CreateOrderDto>().ReverseMap();
                CreateMap<Order, UpdateOrderDto>().ReverseMap();

                // OrderItem Mapping
                CreateMap<OrderItem, OrderItemDto>().ReverseMap();
                CreateMap<OrderItem, CreateOrderItemDto>().ReverseMap();
                CreateMap<OrderItem, UpdateOrderItemDto>().ReverseMap();

                // ProductCategory Mapping
                CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
                CreateMap<ProductCategory, CreateProductCategoryDto>().ReverseMap();
            }
        }
    }
}