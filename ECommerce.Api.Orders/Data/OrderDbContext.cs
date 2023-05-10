using ECommerce.Api.Orders.Db;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}