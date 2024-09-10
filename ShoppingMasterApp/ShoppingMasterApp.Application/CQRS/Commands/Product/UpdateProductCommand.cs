using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Product
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Sku { get; set; }

        public class Handler : IRequestHandler<UpdateProductCommand,Unit>
        {
            private readonly IProductRepository _productRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IProductRepository productRepository, IUnitOfWork unitOfWork)
            {
                _productRepository = productRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(request.Id);
                if (product == null)
                {
                    throw new KeyNotFoundException("Product not found");
                }

                product.Name = request.Name;
                product.Price = new Money(request.Price, "USD");
                product.Stock = request.Stock;
                product.CategoryId = request.CategoryId;
                product.Description = request.Description;
                product.ProductDetails = new ProductDetails(request.Manufacturer, request.Sku);

                _productRepository.Update(product);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
