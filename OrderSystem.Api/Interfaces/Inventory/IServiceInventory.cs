
using OrderSystem.Api.DTOs.Inventory;

namespace OrderSystem.Api.Interfaces
{
    public interface IServiceInventory
    {
        Task<List<GetInventoryDto>> GetInvetoryAsync();
        Task<GetInventoryByIdDto?> GetInventoryByIdAsync(int id);
        Task<GetInventoryDto> CreateInventroyAsync(CreateInventoryDto dto);
        Task<UpdateInventoryDto?> UpdateInvetoryAsync(int id, UpdateInventoryDto dto);
        Task<bool> DeleteInventoryAsync(int id);
    }
}