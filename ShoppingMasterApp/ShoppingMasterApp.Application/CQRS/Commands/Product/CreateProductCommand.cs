namespace ShoppingMasterApp.Application.CQRS.Commands.Product
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        // Add ProductDetails properties
        public string Manufacturer { get; set; }
        public string Sku { get; set; }
    }
}
