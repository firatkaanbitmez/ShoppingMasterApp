using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Money value object conversion
            var moneyConverter = new ValueConverter<Money, decimal>(
                v => v.Amount, // Converts Money to decimal for storage
                v => new Money(v, "USD")); // Converts decimal back to Money (USD as default currency)

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasConversion(moneyConverter)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
                .OwnsOne(p => p.PaymentDetails, pd =>
                {
                    pd.Property(p => p.CardType).HasColumnName("CardType");
                    pd.Property(p => p.CardNumber).HasColumnName("CardNumber");
                    pd.Property(p => p.ExpiryDate).HasColumnName("ExpiryDate");
                });

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasConversion(moneyConverter)
                .HasColumnType("decimal(18,2)");

            // Shipping Address configuration
            modelBuilder.Entity<Shipping>()
                .OwnsOne(s => s.ShippingAddress, sa =>
                {
                    sa.Property(a => a.AddressLine1).HasColumnName("AddressLine1");
                    sa.Property(a => a.AddressLine2).HasColumnName("AddressLine2");
                    sa.Property(a => a.City).HasColumnName("City");
                    sa.Property(a => a.State).HasColumnName("State");
                    sa.Property(a => a.PostalCode).HasColumnName("PostalCode");
                    sa.Property(a => a.Country).HasColumnName("Country");
                });


            modelBuilder.Entity<User>()
               .OwnsOne(u => u.Email, e =>
               {
                   e.Property(p => p.Value)
                    .HasColumnName("Email")
                    .IsRequired();
               });

            // User Address configuration (Value Object)
            modelBuilder.Entity<User>()
                .OwnsOne(u => u.Address, address =>
                {
                    address.Property(a => a.AddressLine1).HasColumnName("AddressLine1");
                    address.Property(a => a.AddressLine2).HasColumnName("AddressLine2");
                    address.Property(a => a.City).HasColumnName("City");
                    address.Property(a => a.State).HasColumnName("State");
                    address.Property(a => a.PostalCode).HasColumnName("PostalCode");
                    address.Property(a => a.Country).HasColumnName("Country");
                });

            // Order TotalAmount for Money type
            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.TotalAmount, m =>
                {
                    m.Property(p => p.Amount).HasColumnName("Amount");
                    m.Property(p => p.Currency).HasColumnName("Currency");
                });
            modelBuilder.Entity<Product>()
                 .OwnsOne(p => p.ProductDetails, pd =>
                 {
                     // Define properties for ProductDetails here if needed
                 });
            base.OnModelCreating(modelBuilder);
        }
    }
}
