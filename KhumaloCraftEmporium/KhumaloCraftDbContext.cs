using Microsoft.EntityFrameworkCore;
using KhumaloCraftEmporium.Models;

namespace KhumaloCraftEmporium.Data
{
    public class KhumaloCraftDbContext : DbContext
    {
        public KhumaloCraftDbContext(DbContextOptions<KhumaloCraftDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal types with appropriate precision and scale
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as necessary

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as necessary

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as necessary
        }
    }
}
