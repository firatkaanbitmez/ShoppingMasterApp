using MediatR;
using ProductEntity = ShoppingMasterApp.Domain.Entities.Product;

namespace ShoppingMasterApp.Application.CQRS.Commands.Product
{
    public class CreateProductCommand : IRequest<ProductEntity>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
    }
}
