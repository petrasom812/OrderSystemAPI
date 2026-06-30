using Microsoft.AspNetCore.Mvc;
using OrderSystem.Api.DTOs.Inventory;
using OrderSystem.Api.Interfaces;

namespace OrderSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IServiceInventory _service;
        public InventoryController(IServiceInventory service)
        {
            _service = service;
        }
        [HttpGet]//Get all inventory
        public async Task <ActionResult> GetAllInventory()
        {
            var getAllInventory = await _service.GetInvetoryAsync();
            return Ok(getAllInventory);
        }
        [HttpGet("{id}")] //Get by Id
        public async Task<ActionResult> GetInventoryById(int id)
        {
            var getInvetoryById = await _service.GetInventoryByIdAsync(id);
            return getInvetoryById == null ? NotFound() : Ok(getInvetoryById);
        }
        [HttpPost]//Create inventory
        public async Task<ActionResult> CreateInventory(CreateInventoryDto dto)
        {
            var createInventory = await _service.CreateInventroyAsync(dto);
            return Ok(createInventory);
        }
        [HttpPut("{id}")] //Update Inventory
        public async Task<IActionResult> UpdateInventory(int id, UpdateInventoryDto dto)
        {
            var updateInventory = await _service.UpdateInvetoryAsync(id, dto);
            return updateInventory == null ? NotFound() : Ok(updateInventory);
        }
        [HttpDelete("{id}")] //delete inventory
        public async Task<IActionResult> DeleteInventory(int id)
        {
            await _service.DeleteInventoryAsync(id);
            return NoContent();
        }
    }
}