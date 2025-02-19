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
    public class ProductController : ControllerBase
    {
        private readonly ECommerceDbContext _context;

        public ProductController(ECommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity
                })
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            // Validate request body
            if (createProductDto == null)
            {
                return BadRequest(new { message = "Invalid request. Product data is required." });
            }

            // Validate price and stock quantity
            if (createProductDto.Price < 0)
            {
                return BadRequest(new { message = "Price must be a non-negative value." });
            }

            if (createProductDto.StockQuantity < 0)
            {
                return BadRequest(new { message = "Stock quantity must be a non-negative value." });
            }

            // Ensure the product does not already exist
            bool productExists = await _context.Products.AnyAsync(p => p.Name == createProductDto.Name);
            if (productExists)
            {
                return BadRequest(new { message = "Product already exists. Please use the update API instead." });
            }

            // Create new product
            var product = new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                StockQuantity = createProductDto.StockQuantity
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Prepare DTO for response
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productDto);
        }

        // PUT: api/product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            // Validate that the ID in the URL matches the ID in the body (if included)
            if (updateProductDto.Id != 0 && updateProductDto.Id != id)
            {
                return BadRequest(new { message = "The product ID in the URL does not match the product ID in the request body." });
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found." });
            }

            // Validate non-negative price and stock quantity
            if (updateProductDto.Price < 0)
            {
                return BadRequest(new { message = "Price must be a non-negative value." });
            }

            //if (updateProductDto.StockQuantity < 0)
            //{
            //    return BadRequest(new { message = "Stock quantity must be a non-negative value." });
            //}

            // Ensure product name is unique (excluding itself)
            bool isDuplicateName = await _context.Products.AnyAsync(p => p.Name == updateProductDto.Name && p.Id != id);
            if (isDuplicateName)
            {
                return BadRequest(new { message = "A product with this name already exists. Please choose a different name." });
            }

            // Update product details
            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Price = updateProductDto.Price;
            product.StockQuantity = updateProductDto.StockQuantity;

            try
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "Conflict detected while updating the product. Please try again." });
            }

            return Ok(new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            });
        }


        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteProduct(int id)
        {
            // Check if the provided ID is valid
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid product ID." });
            }

            var product = await _context.Products.FindAsync(id);

            // If the product is not found, return a 404 response
            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            // Return a JSON response after successful deletion
            return Ok(new { message = $"Product with ID {id} has been successfully deleted." });
        }

    }
}