using Microsoft.EntityFrameworkCore;
using OrderSystem.Api.Models;
using OrderSystem.Api.Models.Inventory;

namespace OrderSystem.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<OrderModel> Orders {get; set;}
        public DbSet<OrderItemModel> OrderItems {get; set;}
        public DbSet<ProductModel> Products {get; set;}
        public DbSet<InventoryModel> Inventories {get; set;}
    }
}