using OrderSystem.Api.DTOs;
namespace OrderSystem.Api.Interface
{
    public interface IServiceOrder
    {
        Task<List<GetOrderDto>> GetOrderAsync();
        Task<GetOrderDetailDto?> GetOrderDetailByIdAsync(int id);
        Task<GetOrderDetailDto> AddOrderAsync(AddOrderDto dto);
        Task<UpdateOrderDto?> UpdateOrderAsync(int id, UpdateOrderDto dto);
        Task<bool> DeleteOrderAsync(int id);
    }
}