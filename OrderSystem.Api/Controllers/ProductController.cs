using OrderSystem.Api.Interfaces.Product;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Api.DTOs.Product;

namespace OrderSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IServiceProduct _service;
        public ProductController(IServiceProduct service)
        {
            _service = service;
        }

        [HttpGet] //Get all Product
        public async Task<ActionResult> GetProduct()
        {
            var getProduct = await _service.GetProductAsync();
            return Ok(getProduct);
        }
        [HttpGet("{id}")] //Get Product by ID
        public async Task<ActionResult> GetProductById(int id)
        {
            var getProductById = await _service.GetProductByIdAsync(id);

            return getProductById == null ? NotFound() : Ok(getProductById);
        }
        [HttpPost]
        public async Task<ActionResult> PostProduct(CreateProductDto dto)
        {
            var createProduct = await _service.CreateProductAsync(dto);
            return Ok(createProduct);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, UpdateProductDto dto)
        {
            var updateProduct = await _service.UpdateProductAsync(id, dto);
            return updateProduct == null ? NotFound() : Ok(updateProduct);
        }
        [HttpDelete("{id}")] //Soft Delete Product : Inactive
        public async Task<ActionResult> SoftDeleteProduct(int id)
        {
            await _service.SoftDeleteProductAsync(id);
            return NoContent();
        }

    }
}