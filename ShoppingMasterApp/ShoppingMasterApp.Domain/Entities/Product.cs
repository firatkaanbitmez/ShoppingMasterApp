using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }
        public int Stock { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
        public ProductDetails ProductDetails { get; private set; }
        public ICollection<Review> Reviews { get; private set; }

        public Product() { } // Parameterless constructor for EF Core

        // Primary constructor
        public Product(string name, string description, Money price, int stock, int categoryId, ProductDetails productDetails)
        {
            SetName(name);
            SetDescription(description);
            SetPrice(price);
            SetStock(stock);
            CategoryId = categoryId;
            ProductDetails = productDetails ?? throw new ArgumentNullException(nameof(productDetails));
            Reviews = new List<Review>();
        }
        public void SetProductDetails(ProductDetails productDetails)
        {
            ProductDetails = productDetails ?? throw new ArgumentNullException(nameof(productDetails));
        }

        // Fluent-style setters for updating properties
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Product name cannot be empty.");
            Name = name;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description)) throw new ArgumentException("Description cannot be empty.");
            Description = description;
        }

        public void SetPrice(Money price)
        {
            if (price == null || price.Amount <= 0) throw new ArgumentException("Price must be a valid positive value.");
            Price = price;
        }

        public void SetStock(int stock)
        {
            if (stock < 0) throw new ArgumentException("Stock cannot be negative.");
            Stock = stock;
        }

        public void AddReview(Review review)
        {
            if (review == null) throw new ArgumentNullException(nameof(review));
            Reviews.Add(review);
        }
    }
}