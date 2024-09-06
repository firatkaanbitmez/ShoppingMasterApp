using Microsoft.EntityFrameworkCore;
using ShoppingMasterApp.Domain.Entities;

namespace ShoppingMasterApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        // OnModelCreating metodu burada tanımlanıyor
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); // 18 basamak, 2 ondalık basamak

            base.OnModelCreating(modelBuilder);
        }
    }
}
