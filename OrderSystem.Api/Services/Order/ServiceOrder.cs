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

        private const string Cancelled = "Cancelled";
        private const string Shipped = "Shipped";

        //Get Method: Get all orders
        public async Task<List<GetOrderDto>> GetOrderAsync()
        {
            var orders = await _context.Orders.AsNoTracking().ToListAsync();

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
            var order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
            return order != null ? MapToDetailDto(order) : null;
        }
        //POST Method: Add Order
        public async Task<GetOrderDetailDto> AddOrderAsync(AddOrderDto dto)
        {
            ValidateString(dto.Status);

            var products = await _context.Products
                .Where(p => dto.Items.Select(i => i.ProductId)
                .Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            //Validate items
            if (dto.Items == null || !dto.Items.Any())
                throw new ArgumentException("Order must contains at least one item.");
            var order = new OrderModel
            {
                TotalAmount = TotalAmountCal(dto.Items),
                OrderNumber = OrderNumberGenerator.GenerateOrderNumber(),
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                EditedAt = null,
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();


            var orderItems = dto.Items.Select(i =>
            {
                if (!products.ContainsKey(i.ProductId))
                    throw new ArgumentException(
                        $"Product {i.ProductId} not found.");

                return new OrderItemModel
                {
                    OrderId = order.Id,
                    ProductId = i.ProductId,
                    Sku = products[i.ProductId].Sku,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                };
            }).ToList();

            return MapToDetailDto(order);
        }

        //PUT Method: Update order detail
        public async Task<UpdateOrderDto?> UpdateOrderAsync(int id, UpdateOrderDto dto)
        {

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return null;
            if (order.Status == Cancelled || order.Status == Shipped)
                return null;

            ValidateString(dto.Status);
            order.Status = dto.Status;
            order.EditedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new UpdateOrderDto
            {
                Status = order.Status
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

        //Reusable StringValidation
        public string ValidateString(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Value can not be null or whitespace.");
            return status;
        }

        //reusable mapper
        private static GetOrderDetailDto MapToDetailDto(OrderModel o) => new()
        {
            Id = o.Id,
            OrderNumber = o.OrderNumber,
            TotalAmount = o.TotalAmount,
            Status = o.Status,
            CreatedAt = o.CreatedAt,
            EditedAt = o.EditedAt
        };

        //reusable total Amount
        private decimal TotalAmountCal(List<OrderItemDto> items)
        {
            return items.Sum(i => i.Quantity * i.UnitPrice);
        }
    }
}