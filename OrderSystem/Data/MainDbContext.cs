using Microsoft.EntityFrameworkCore;
using OrderSystem.Models;


namespace OrderSystem.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; } = default!; //ignore null warning
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Status> Statuses { get; set; } = default!;
    }
}
