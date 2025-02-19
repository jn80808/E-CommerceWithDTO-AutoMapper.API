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
    public class ProductCategoryController : ControllerBase
    {
        private readonly ECommerceDbContext _context;
        private readonly IMapper _mapper;

        public ProductCategoryController(ECommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/productcategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories()
        {
            var productCategories = await _context.ProductCategories.ToListAsync();
            return Ok(_mapper.Map<List<ProductCategoryDto>>(productCategories));
        }

        // GET: api/productcategory/5/10
        [HttpGet("{productId}/{categoryId}")]
        public async Task<ActionResult<ProductCategoryDto>> GetProductCategory(int productId, int categoryId)
        {
            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);

            if (productCategory == null) return NotFound("Product-Category relationship not found.");

            return Ok(_mapper.Map<ProductCategoryDto>(productCategory));
        }

        // POST: api/productcategory
        [HttpPost]
        public async Task<ActionResult<ProductCategoryDto>> CreateProductCategory(CreateProductCategoryDto createProductCategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Ensure the product and category exist before adding relationship
            var productExists = await _context.Products.AnyAsync(p => p.Id == createProductCategoryDto.ProductId);
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == createProductCategoryDto.CategoryId);

            if (!productExists) return BadRequest("Invalid ProductId.");
            if (!categoryExists) return BadRequest("Invalid CategoryId.");

            // Map and save the new product-category relationship
            var productCategory = _mapper.Map<ProductCategory>(createProductCategoryDto);
            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductCategory),
                new { productId = productCategory.ProductId, categoryId = productCategory.CategoryId },
                _mapper.Map<ProductCategoryDto>(productCategory));
        }

        // DELETE: api/productcategory/5/10
        [HttpDelete("{productId}/{categoryId}")]
        public async Task<IActionResult> DeleteProductCategory(int productId, int categoryId)
        {
            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);

            if (productCategory == null) return NotFound("Product-Category relationship not found.");

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
