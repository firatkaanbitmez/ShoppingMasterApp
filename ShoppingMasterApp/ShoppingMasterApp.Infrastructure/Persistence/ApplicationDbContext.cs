using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Domain.Entities;

namespace ShoppingMasterApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet tanımları
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
            modelBuilder.Entity<User>(builder =>
            {
                builder.OwnsOne(u => u.Address);
                builder.OwnsOne(u => u.Email); 
            });
            modelBuilder.Entity<Shipping>(builder =>
            {
                builder.OwnsOne(s => s.ShippingAddress);
            });
            modelBuilder.Entity<Shipping>(builder =>
            {
                builder.OwnsOne(s => s.ShippingAddress);
            });
            modelBuilder.Entity<Order>(builder =>
            {
                builder.OwnsOne(o => o.TotalAmount);  
            });
            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.TotalAmount, builder =>
                {
                    builder.Property(m => m.Amount).HasColumnType("decimal(18,2)");
                });
            modelBuilder.Entity<Product>()
                  .OwnsOne(p => p.Price, builder =>
                  {
                      builder.Property(m => m.Amount).HasColumnType("decimal(18,2)");
                  });

            modelBuilder.Entity<Payment>()
                .OwnsOne(p => p.Amount, builder =>
                {
                    builder.Property(m => m.Amount).HasColumnType("decimal(18,2)");
                });

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Discount>()
                .Property(d => d.Amount)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Payment>(builder =>
            {
                builder.OwnsOne(o => o.Amount);
            });
            modelBuilder.Entity<Payment>(builder =>
            {
                builder.OwnsOne(p => p.PaymentDetails); 
            });

            modelBuilder.Entity<Product>(builder =>
            {
                builder.OwnsOne(p => p.Price);
                builder.OwnsOne(p => p.ProductDetails);

            });
            base.OnModelCreating(modelBuilder);

          
        }
    }
}
