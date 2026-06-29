using OrderSystem.Api.Models;
using OrderSystem.Api.DTOs;
using OrderSystem.Api.Data;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Api.Interface;

namespace OrderSystem.Api.Services
{
    public class ServiceOrder : IServiceOrder
    {
        private readonly AppDbContext _context;
        public ServiceOrder(AppDbContext context)
        {
            _context = context;
        }

        //Get Method: Get all orders
        public async Task<List<GetOrderDto>> GetOrderAsync()
        {
            var orders = await _context.Orders.ToListAsync();

            return orders.Select(o => new GetOrderDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                TotalAmount = o.TotalAmount
            }).ToList();
        }
        //Get Method: Get Order by Id
        public async Task<GetOrderDetailDto?> GetOrderDetailByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            return order != null ? new GetOrderDetailDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                EditedAt = order.EditedAt
            } : null;
        }
        //POST Method: Add Order
        public async Task<GetOrderDetailDto> AddOrderAsync(decimal totalAmount, string status)
        {
            var order = new OrderModel
            {
                TotalAmount = totalAmount,
                OrderNumber = OrderNumberGenerator.GenerateOrderNumber(),
                Status = statusHandler(status),
                CreatedAt = DateTime.UtcNow,
                EditedAt = null,
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return new GetOrderDetailDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                EditedAt = order.EditedAt
            };

        }

        //PUT Method: Update order detail
        public async Task<UpdateOrderDto?> UpdateOrderAsync(int id, decimal totalAmount, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return null;
            if (order.Status == "Completed" || order.Status == "Cancelled")
                return null;

            order.TotalAmount = totalAmount;
            order.Status = statusHandler(status);
            order.EditedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new UpdateOrderDto
            {
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                EditedAt = order.EditedAt
            };
        }

        //DELETE Method: Delete Order

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }
        public string statusHandler(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Value can not be null or whitespace.");
            return status;
        }
    }
}