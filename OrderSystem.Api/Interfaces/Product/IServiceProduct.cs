using OrderSystem.Api.DTOs.Product;

namespace OrderSystem.Api.Interfaces.Product
{
    public interface IServiceProduct
    {
        Task<List<GetProductDto>> GetProductAsync();
        Task <GetProductByIdDto?> GetProductByIdAsync(int id);
        Task<GetProductDto> CreateProductAsync(CreateProductDto dto);
        Task<UpdateProductDto?> UpdateProductAsync(int id, UpdateProductDto dto);
        Task SoftDeleteProductAsync(int id);
    }
}