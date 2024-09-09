using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Domain.Entities;

namespace ShoppingMasterApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet definitions
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User entity
            modelBuilder.Entity<User>(builder =>
            {
                builder.OwnsOne(u => u.Address);
                builder.OwnsOne(u => u.Email);
            });

            // Shipping entity
            modelBuilder.Entity<Shipping>(builder =>
            {
                builder.OwnsOne(s => s.ShippingAddress);
            });

            // Order entity
            modelBuilder.Entity<Order>(builder =>
            {
                builder.OwnsOne(o => o.TotalAmount, config =>
                {
                    config.Property(m => m.Amount).HasColumnType("decimal(18,2)");
                });
            });

            // Product entity
            modelBuilder.Entity<Product>(builder =>
            {
                builder.OwnsOne(p => p.Price, config =>
                {
                    config.Property(m => m.Amount).HasColumnName("Price_Amount").HasColumnType("decimal(18,2)");
                    config.Property(m => m.Currency).HasColumnName("Price_Currency");
                });
                builder.OwnsOne(p => p.ProductDetails);
            });

            // Payment entity
            modelBuilder.Entity<Payment>(builder =>
            {
                builder.OwnsOne(p => p.Amount, config =>
                {
                    config.Property(m => m.Amount).HasColumnType("decimal(18,2)");
                });
                builder.OwnsOne(p => p.PaymentDetails);
            });

            // OrderItem entity
            modelBuilder.Entity<OrderItem>(builder =>
            {
                builder.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");
            });

            // Discount entity
            modelBuilder.Entity<Discount>(builder =>
            {
                builder.Property(d => d.Amount).HasColumnType("decimal(18,2)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
