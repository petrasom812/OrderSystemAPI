using Microsoft.EntityFrameworkCore;
using OrderSystem.Api.Data;
using OrderSystem.Api.DTOs.Inventory;
using OrderSystem.Api.Interfaces;
using OrderSystem.Api.Models.Inventory;
namespace OrderSystem.Api.Services.Inventory
{
    public class ServiceInventory : IServiceInventory
    {
        private readonly AppDbContext _context;
        public ServiceInventory(AppDbContext context)
        {
            _context = context;
        }
        //Get all inventory
        public async Task<List<GetInventoryDto>> GetInvetoryAsync()
        {
            var getInventories = await _context.Inventories
                .AsNoTracking()
                .ToListAsync();

            return getInventories
                .Select(MapToInvetoryDto)
                .ToList();
        }
        //Get Inventory by Id
        public async Task<GetInventoryByIdDto?> GetInventoryByIdAsync(int id)
        {
            var getInventoryById = await _context.Inventories.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            return getInventoryById == null ? null : MapToInvetoryByIdDto(getInventoryById);
        }
        //Create Inventory
        public async Task<GetInventoryDto> CreateInventroyAsync(CreateInventoryDto dto)
        {
            var productExists = await _context.Products
                .AnyAsync(p => p.Id == dto.ProductId && p.IsActive);
            if(!productExists)
                throw new ArgumentException("Product not found");
            var inventoryExists = await _context.Inventories
                .AnyAsync(i => i.ProductId == dto.ProductId);
            if(inventoryExists)
                throw new ArgumentException("Inventory already exists for this prodyct.");

            var createInventory = new InventoryModel
            {
                ProductId = dto.ProductId,
                QuantityOnHand = dto.QuantityOnHand,
                ReorderLevel = dto.ReorderLevel,
                CreatedAt = DateTime.UtcNow,
                EditedAt = null  
            };
            _context.Inventories.Add(createInventory);
            await _context.SaveChangesAsync();

            return MapToInvetoryDto(createInventory);
        }
        //Update inventory
        public async Task<UpdateInventoryDto?> UpdateInvetoryAsync(int id, UpdateInventoryDto dto)
        {
            var updateInvetory = await _context.Inventories.FindAsync(id);
            if(updateInvetory == null)
                return null;
            
            updateInvetory.QuantityOnHand = dto.QuantityOnHand;
            updateInvetory.ReorderLevel = dto.ReorderLevel;
            updateInvetory.EditedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return new UpdateInventoryDto
            {
                QuantityOnHand = dto.QuantityOnHand,
                ReorderLevel = dto.ReorderLevel
            };
        }
        //Delete Inventory
        public async Task<bool> DeleteInventoryAsync(int id)
        {
            var deleteInvetory = await _context.Inventories.FindAsync(id);
            if(deleteInvetory == null)
                return false;
            _context.Inventories.Remove(deleteInvetory);
            await _context.SaveChangesAsync();
            return true;
        }
        //Get all Method
        private static GetInventoryDto MapToInvetoryDto(InventoryModel i) => new()
        {
            Id = i.Id,
            ProductId = i.ProductId,
            QuantityOnHand = i.QuantityOnHand,
            ReorderLevel = i.ReorderLevel,
            CreatedAt = i.CreatedAt,
            EditedAt = i.EditedAt
        };
        //Get by Id
        private static GetInventoryByIdDto MapToInvetoryByIdDto(InventoryModel i) => new()
        {
            Id = i.Id,
            ProductId = i.ProductId,
            QuantityOnHand = i.QuantityOnHand,
            ReorderLevel = i.ReorderLevel,
            CreatedAt = i.CreatedAt,
            EditedAt = i.EditedAt
        };
    }
}