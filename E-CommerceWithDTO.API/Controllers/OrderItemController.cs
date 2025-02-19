using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Commerce.API.Models.Domain;
using E_Commerce.API.Models.DTO;
using ECommerceSystem;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly ECommerceDbContext _context;
        private readonly IMapper _mapper;

        public OrderItemController(ECommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/orderitem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItems()
        {
            var orderItems = await _context.OrderItems.ToListAsync();
            return Ok(_mapper.Map<List<OrderItemDto>>(orderItems));
        }

        // GET: api/orderitem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDto>> GetOrderItem(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderItemDto>(orderItem));
        }

        // POST: api/orderitem
        [HttpPost]
        public async Task<ActionResult<OrderItemDto>> CreateOrderItem([FromBody] CreateOrderItemDto createOrderItemDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var orderExists = await _context.Orders.AnyAsync(o => o.Id == createOrderItemDto.OrderId);
            if (!orderExists) return BadRequest("Invalid OrderId.");

            var productExists = await _context.Products.AnyAsync(p => p.Id == createOrderItemDto.ProductId);
            if (!productExists) return BadRequest("Invalid ProductId.");

            var orderItem = _mapper.Map<OrderItem>(createOrderItemDto);

            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderItem), new { id = orderItem.Id }, _mapper.Map<OrderItemDto>(orderItem));
        }

        // PUT: api/orderitem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] UpdateOrderItemDto updateOrderItemDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null) return NotFound();

            _mapper.Map(updateOrderItemDto, orderItem);
            _context.Entry(orderItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/orderitem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
