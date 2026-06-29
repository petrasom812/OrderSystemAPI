using OrderSystem.Api.DTOs;
namespace OrderSystem.Api.Interface
{
    public interface IServiceOrder
    {
        Task<List<GetOrderDto>> GetOrderAsync();
        Task<GetOrderDetailDto?> GetOrderDetailByIdAsync(int id);
        Task<GetOrderDetailDto> AddOrderAsync(decimal totalAmount, string status);
        Task<UpdateOrderDto?> UpdateOrderAsync(int id, decimal totalAmount, string status);
        Task<bool> DeleteOrderAsync(int id);
    }
}