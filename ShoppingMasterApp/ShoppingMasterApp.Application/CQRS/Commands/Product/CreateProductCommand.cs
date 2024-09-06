using MediatR;
using ProductEntity = ShoppingMasterApp.Domain.Entities.Product;

namespace ShoppingMasterApp.Application.CQRS.Commands.Product
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

}
