using Microsoft.AspNetCore.Mvc;
using OrderSystem.Api.Services;
using OrderSystem.Api.DTOs;

namespace OrderSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ServiceOrder _serivices;
        public OrderController(ServiceOrder service)
        {
            _serivices = service;
        }

        [HttpGet] // Get Order
        public async Task<ActionResult> GetOrder()
        {
            var order = await _serivices.GetOrder();
            return Ok(order);
        }
        [HttpGet("{id}")] // Get Order Detail by ID
        public async Task<ActionResult?> GetOrderDetailById(int id)
        {
            var order = await _serivices.GetOrderDetailById(id);
            if (order == null)
                return NotFound("Order not found");
            return CreatedAtAction(nameof(GetOrderDetailById), new {Id = order.Id}, order);
        }
        [HttpPost]
        public async Task<ActionResult> PostOrder(AddOrderDto dto)
        {
            var order = await _serivices.AddOrder(dto.TotalAmount, dto.Status);
            return Ok(order); 
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, UpdateOrderDto dto)
        {
            var order = await _serivices.UpdateOrder(id, dto.TotalAmount, dto.Status);
            return order == null ? NotFound("Order not found.") : Ok("Order information updated.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _serivices.DeleteOrder(id);
            return !order ? NotFound() : NoContent();
        }
    }
}