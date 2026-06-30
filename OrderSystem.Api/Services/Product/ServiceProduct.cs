using OrderSystem.Api.Models;
using OrderSystem.Api.Data;
using OrderSystem.Api.DTOs.Product;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Api.Interfaces.Product;

namespace OrderSystem.Api.Services.Product
{
    public class ServiceProduct : IServiceProduct
    {
        private readonly AppDbContext _context;
        public ServiceProduct(AppDbContext context)
        {
            _context = context;
        }

        //Get All Product
        public async Task<List<GetProductDto>> GetProductAsync()
        {
            var getProducts = await _context.Products
                .AsNoTracking()
                .Where(p => p.IsActive)
                .ToListAsync();

            return getProducts
                .Select(MapToProductDto)
                .ToList();
        }
        //Get Product by ID
        public async Task <GetProductByIdDto?> GetProductByIdAsync(int id)
        {
            var getProductById = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if(getProductById == null || !getProductById.IsActive)
                return null!;

            return new GetProductByIdDto
            {
                Name = getProductById.Name,
                Sku = getProductById.Sku,
                IsActive = getProductById.IsActive
            };
            
        }
        //Create Product
        public async Task<GetProductDto> CreateProductAsync(CreateProductDto dto)
        {
            ValidateString(dto.Sku);
            ValidateString(dto.Name);
            await EnsureSkuUnique(dto.Sku);
            var addProduct = new ProductModel
            {
                Sku = dto.Sku,
                Name = dto.Name,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                EditedAt = null
            };

            _context.Products.Add(addProduct);
            await _context.SaveChangesAsync();

            return MapToProductDto(addProduct);
        }
        //Update Product
        public async Task<UpdateProductDto?> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var updateProduct = await _context.Products.FindAsync(id);

            if(updateProduct == null || !updateProduct.IsActive)
                return null;

            ValidateString(dto.Name);
            updateProduct.Name = dto.Name;
            updateProduct.EditedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return new UpdateProductDto
            {
                Name = dto.Name
            };
        }
        //Soft Delete Product
        public async Task SoftDeleteProductAsync(int id)
        {
            var softDeleteProduct = await _context.Products.FindAsync(id);
            if(softDeleteProduct == null)
                throw new ArgumentException("Product not found.");
            if(!softDeleteProduct.IsActive)
                throw new ArgumentException("Product already inactive.");
            softDeleteProduct.IsActive = false;
            await _context.SaveChangesAsync();
        }
        
        //Reusable Mapper
        private static GetProductDto MapToProductDto(ProductModel p) => new()
        {
            Id = p.Id,
            Sku = p.Sku,
            Name = p.Name,
            IsActive = p.IsActive,
            CreatedAt = p.CreatedAt,
            EditedAt = p.EditedAt
        };

        public string ValidateString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value can not be null or whitespace.");
            return value;
        }

        private async Task EnsureSkuUnique(string sku)
        {
            var exists = await _context.Products.AnyAsync(p => p.Sku == sku);
            if(exists)
                throw new ArgumentException("SKU already exists.");
        }
    }
}