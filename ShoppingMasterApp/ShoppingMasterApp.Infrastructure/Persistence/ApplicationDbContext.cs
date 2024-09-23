using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.ValueObjects;

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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer entity configuration
            modelBuilder.Entity<Customer>(builder =>
            {
                builder.OwnsOne(u => u.Address, address =>
                {
                    address.Property(a => a.AddressLine1).HasColumnName("AddressLine1");
                    address.Property(a => a.City).HasColumnName("City");
                    address.Property(a => a.State).HasColumnName("State");
                    address.Property(a => a.PostalCode).HasColumnName("PostalCode");
                    address.Property(a => a.Country).HasColumnName("Country");
                });
                builder.OwnsOne(u => u.PhoneNumber, phone =>
                {
                    phone.Property(p => p.CountryCode).HasColumnName("Phone_CountryCode").IsRequired();
                    phone.Property(p => p.Number).HasColumnName("Phone_Number").IsRequired();
                });
                builder.OwnsOne(u => u.Email, email =>
                {
                    email.Property(e => e.Value).HasColumnName("Email").IsRequired();
                });

                builder.Property(c => c.Id).ValueGeneratedOnAdd();
                builder.Property(c => c.IdGuid).HasDefaultValueSql("NEWID()");
            });

            // Admin entity configuration
            modelBuilder.Entity<Admin>(builder =>
            {
                builder.OwnsOne(u => u.Email, email =>
                {
                    email.Property(e => e.Value).HasColumnName("Email").IsRequired();
                });
                builder.OwnsOne(u => u.PhoneNumber, phone =>
                {
                    phone.Property(p => p.CountryCode).HasColumnName("Phone_CountryCode").IsRequired();
                    phone.Property(p => p.Number).HasColumnName("Phone_Number").IsRequired();
                });

                builder.Property(c => c.Id).ValueGeneratedOnAdd();
                builder.Property(c => c.IdGuid).HasDefaultValueSql("NEWID()");
            });

            // Product entity configuration
            modelBuilder.Entity<Product>(builder =>
            {
                builder.OwnsOne(p => p.Price, config =>
                {
                    config.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("Price_Amount");
                    config.Property(m => m.Currency).HasColumnName("Price_Currency");
                });

                builder.OwnsOne(p => p.ProductDetails, config =>
                {
                    config.Property(pd => pd.Manufacturer).HasColumnName("Manufacturer");
                    config.Property(pd => pd.Sku).HasColumnName("Sku");
                });
            });

            // Cart entity configuration
            modelBuilder.Entity<Cart>(builder =>
            {
                builder.HasKey(c => c.Id);
                builder.Property(c => c.Id).ValueGeneratedOnAdd();
                builder.HasMany(c => c.CartItems)
                       .WithOne()
                       .HasForeignKey(ci => ci.CartId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            // Order entity configuration
            modelBuilder.Entity<Order>(builder =>
            {
                builder.OwnsOne(o => o.TotalAmount, config =>
                {
                    config.Property(m => m.Amount).HasColumnName("TotalAmount_Amount").HasColumnType("decimal(18,2)");
                    config.Property(m => m.Currency).HasColumnName("TotalAmount_Currency");
                });
                builder.HasMany(o => o.OrderItems)
                       .WithOne()
                       .HasForeignKey(oi => oi.OrderId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            // Payment entity configuration
            modelBuilder.Entity<Payment>(builder =>
            {
                builder.OwnsOne(p => p.Amount, config =>
                {
                    config.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("Payment_Amount");
                    config.Property(m => m.Currency).HasColumnName("Payment_Currency");
                });

                builder.OwnsOne(p => p.PaymentDetails, config =>
                {
                    config.Property(pd => pd.CardType).HasColumnName("Card_Type");
                    config.Property(pd => pd.CardNumber).HasColumnName("Card_Number");
                    config.Property(pd => pd.ExpiryDate).HasColumnName("Expiry_Date");
                    config.Property(pd => pd.Cvv).HasColumnName("CVV");
                });
            });

            // Shipping entity configuration
            modelBuilder.Entity<Shipping>(builder =>
            {
                builder.OwnsOne(s => s.ShippingAddress, config =>
                {
                    config.Property(a => a.AddressLine1).HasColumnName("ShippingAddress_Line1");
                    config.Property(a => a.AddressLine2).HasColumnName("ShippingAddress_Line2");
                    config.Property(a => a.City).HasColumnName("ShippingAddress_City");
                    config.Property(a => a.State).HasColumnName("ShippingAddress_State");
                    config.Property(a => a.PostalCode).HasColumnName("ShippingAddress_PostalCode");
                    config.Property(a => a.Country).HasColumnName("ShippingAddress_Country");
                });
            });

            // Additional entities configuration
            modelBuilder.Entity<CartItem>().Property(c => c.UnitPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Discount>().Property(d => d.Amount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(o => o.UnitPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Shipping>().Property(s => s.ShippingCost).HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
