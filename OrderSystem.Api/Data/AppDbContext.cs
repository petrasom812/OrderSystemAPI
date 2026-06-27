using Microsoft.EntityFrameworkCore;
using OrderSystem.Api.Models;

namespace OrderSystem.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<OrderModel> Orders {get; set;}
    }
}