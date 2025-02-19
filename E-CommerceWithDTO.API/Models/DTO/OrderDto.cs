namespace E_Commerce.API.Models.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class CreateOrderDto
    {
        public string CustomerName { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }

    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
    }
}